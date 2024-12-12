using Microsoft.EntityFrameworkCore;
using Quartz;
using socials.DBContext;
using socials.DBContext.DTO.Post;
using socials.DBContext.DTO.Tag;
using socials.DBContext.Models;
using socials.DBContext.Models.Enums;
using socials.Services.IServices;
using socials.SupportiveServices.Exceptions;
using socials.SupportiveServices.Token;
using socials.SupportiveServices.Validations;

namespace socials.Services;

public class PostService(AppDbcontext context, TokenInteractions tokenService, GARContext garContext)
    : IPostService
{
    public async Task<Guid> CreatePost(CreatePostDTO post, string? token)
    {
        var userId = tokenService.GetIdFromToken(token);
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedException("Пользователь не авторизован");
        }

        var user = await context.Users.FirstOrDefaultAsync(d => d.Id == Guid.Parse(userId));

        var address = await garContext.AsAddrObjs.FirstOrDefaultAsync(a => a.Objectguid == post.AddressId);
        if (address == null)
        {
            var house = await garContext.AsHouses.FirstOrDefaultAsync(h => h.Objectguid == post.AddressId);
            if (house == null)
            {
                throw new NotFoundException("Такого адреса не существует");
            }
        }

        if (!TitleValidator.ValidateTitle(post.Title))
        {
            throw new BadRequestException("Длина заголовка должна быть от 1 до 100 символов");
        }

        if (!DescriptionValidator.ValidateDescription(post.Description))
        {
            throw new BadRequestException("Длина описания должна быть от 1 до 3000 символов");
        }

        if (post.ReadingTime < 0)
        {
            throw new BadRequestException("Время чтения не может быть отрицательным");
        }

        if (!UrlValidator.IsValidUrl(post.Image))
        {
            throw new BadRequestException("Некорректный формат ссылки на изображение");
        }

        var newPost = new Post
        {
            Id = Guid.NewGuid(),
            CreateTime = DateTime.UtcNow,
            Title = post.Title,
            Description = post.Description,
            ReadingTime = post.ReadingTime,
            Image = post.Image,
            AuthorId = user.Id,
            Author = user.Name,
            CommunityId = null,
            CommunityName = null,
            AddressId = address.Objectguid,
            Likes = 0,
            HasLike = false,
            CommentsCount = 0,
            Comments = new List<Comment>()
        };

        var existingTags = await context.Tags.Where(t => post.Tags.Contains(t.Id)).ToListAsync();
        if (existingTags.Count != post.Tags.Count)
        {
            throw new NotFoundException("Введите существующие теги");
        }

        foreach (var tag in existingTags)
        {
            newPost.PostTags.Add(new PostTags { PostId = newPost.Id, TagId = tag.Id });
        }

        context.Posts.Add(newPost);
        await context.SaveChangesAsync();
        
        return newPost.Id;
    }

    public async Task<PostFullDTO> GetPostById(Guid id)
    {
        var post = await context.Posts
            .Include(p => p.PostTags)
            .ThenInclude(pt => pt.Tag)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            throw new NotFoundException("Такого поста не существует");
        }

        return new PostFullDTO
        {
            Id = post.Id,
            CreateTime = post.CreateTime,
            Title = post.Title,
            Description = post.Description,
            ReadingTime = post.ReadingTime,
            Image = post.Image,
            AuthorId = post.AuthorId,
            Author = post.Author,
            CommunityId = post.CommunityId,
            CommunityName = post.CommunityName,
            AddressId = post.AddressId,
            Likes = post.Likes,
            HasLike = post.HasLike,
            CommentsCount = post.Comments.Count,
            Tags = post.PostTags.Select(pt => new TagDTO
            {
                Id = pt.Tag.Id,
                CreateTime = pt.Tag.CreateTime,
                Name = pt.Tag.Name
            }).ToList(),
            Comments = post.Comments.Select(c => new Comment
            {
                Id = c.Id,
                CreateTime = c.CreateTime,
                Content = c.Content,
                ModifiedDate = c.ModifiedDate,
                DeleteDate = c.DeleteDate,
                AuthorId = c.AuthorId,
                Author = c.Author,
                SubComments = c.SubComments
            }).ToList()
        };
    }

    public async Task<bool> AddLikeToPost(Guid postId, Guid userId)
    {
        var existingLike = await context.PostLikes
            .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

        if (existingLike != null)
        {
            throw new BadRequestException("Вы уже поставили лайк на этот пост");
        }

        var post = await context.Posts.FindAsync(postId);
        if (post == null)
        {
            throw new NotFoundException("Такого поста не существует");
        }

        var community = await context.Communities.FirstOrDefaultAsync(c => c.Id == post.CommunityId);
        if (community is { IsClosed: true })
        {
            var isMember =
                await context.CommunityUsers.AnyAsync(cu => cu.CommunityId == post.CommunityId && cu.UserId == userId);
            if (!isMember)
            {
                throw new BadRequestException(
                    "Для того, чтобы поставить лайк на пост закрытой группы, нужно быть её подписчиком");
            }
        }

        context.PostLikes.Add(new PostLike { PostId = postId, UserId = userId });
        await context.SaveChangesAsync();

        post.Likes++;
        if (post.Likes > 0)
        {
            post.HasLike = true;
        }

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteLikeFromPost(Guid postId, Guid userId)
    {
        var existingLike =
            await context.PostLikes.FirstOrDefaultAsync(pl => pl.PostId == postId && pl.UserId == userId);

        if (existingLike == null)
        {
            throw new BadRequestException("Вы не ставили лайк на этот пост");
        }

        var post = await context.Posts.FindAsync(postId);
        if (post == null)
        {
            throw new NotFoundException("Такого поста не существует");
        }

        context.PostLikes.Remove(existingLike);
        await context.SaveChangesAsync();

        post.Likes--;
        if (post.Likes == 0)
        {
            post.HasLike = false;
        }

        await context.SaveChangesAsync();

        return true;
    }
    
    public async Task<PaginatedList<PostDTO>> GetPosts(
            string[]? tags,
            string? author,
            int? min,
            int? max,
            SortingOrder sortingOrder,
            bool onlyMyCommunities,
            int page = 1,
            int size = 5)
        {
            if (page <= 0 || size <= 0)
            {
                throw new BadRequestException("Номер страницы и размер страницы должны быть положительными числами");
            }
            
            if (min <= 0 || max <= 0)
            {
                throw new BadRequestException("Параметры времени чтения должны быть положительными числами");
            }
        
            if (tags != null && tags.Length > 0)
            {
                bool atLeastOneTagExists = await context.Tags.AnyAsync(t => tags.Contains(t.Name));
                if (!atLeastOneTagExists)
                {
                    throw new NotFoundException("Ни один из указанных тегов не найден");
                }
            }
            
            if (!string.IsNullOrEmpty(author))
            {
                bool authorExists = await context.Posts.AnyAsync(p => p.Author.Contains(author));
                if (!authorExists)
                {
                    throw new NotFoundException($"Автор с именем или частью имени '{author}' не найден");
                }
            }

            var query = context.Posts
                .Include(p => p.Comments)
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .AsQueryable();

            if (tags != null && tags.Length > 0)
            {
                query = query.Where(p => p.PostTags.Any(pt => tags.Contains(pt.Tag.Name)));
            }

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(p => p.Author.Contains(author));
            }

            if (min.HasValue)
            {
                query = query.Where(p => p.ReadingTime >= min.Value);
            }
            if (max.HasValue)
            {
                query = query.Where(p => p.ReadingTime <= max.Value);
            }

            var token = tokenService.GetTokenFromHeader();
            if (token == null)
            {
                query = query.Where(p => p.Community != null && !p.Community.IsClosed); 
            }
            else
            {
                string stringUserId = tokenService.GetIdFromToken(token);
                if (string.IsNullOrEmpty(stringUserId))
                {
                    throw new UnauthorizedException("");
                }
                Guid userId;
                try
                {
                    userId = Guid.Parse(stringUserId);
                }
                catch (FormatException)
                {
                    throw new UnauthorizedException("");
                }
                query = query.Where(p => 
                        (p.Community != null && !p.Community.IsClosed) ||  
                        (context.CommunityUsers.Any(cu => cu.UserId == userId && cu.CommunityId == p.CommunityId)) // Communities where the user is a member
                );
            }
            
            if (onlyMyCommunities)
            {
                if (token == null)
                {
                    throw new UnauthorizedException("Пользователь не авторизован");
                }
                var stringUserId = tokenService.GetIdFromToken(token);
                Guid userId = Guid.Parse(stringUserId);
                var userCommunityIds = await context.CommunityUsers 
                    .Where(uc => uc.UserId == userId)
                    .Select(uc => uc.CommunityId)
                    .ToListAsync();

                query = query.Where(p => userCommunityIds.Contains(p.CommunityId ?? Guid.Empty));
            }
            
            switch (sortingOrder)
            {
                case SortingOrder.CreateDesc:
                    query = query.OrderByDescending(p => p.CreateTime);
                    break;
                case SortingOrder.CreateAsc:
                    query = query.OrderBy(p => p.CreateTime);
                    break;
                case SortingOrder.LikeAsc:
                    query = query.OrderBy(p => p.Likes);
                    break;
                case SortingOrder.LikeDesc:
                    query = query.OrderByDescending(p => p.Likes);
                    break;
                default:
                    query = query.OrderByDescending(p => p.CreateTime); 
                    break;
            }

            int totalPosts = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalPosts / (double)size);
            if (page > totalPages && totalPosts > 0) 
            {
                throw new NotFoundException("Запрошенная страница не существует");
            }
            
            var posts = await query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(p => new PostDTO
                {
                    Id = p.Id,
                    CreateTime = p.CreateTime,
                    Title = p.Title,
                    Description = p.Description,
                    ReadingTime = p.ReadingTime,
                    Image = p.Image,
                    AuthorId = p.AuthorId,
                    Author = p.Author,
                    CommunityId = p.CommunityId,
                    CommunityName = p.CommunityName,
                    AddressId = p.AddressId,
                    Likes = p.Likes,
                    CommentsCount = p.CommentsCount,
                    Tags = p.PostTags.Select(pt => pt.TagId).ToList(),
                })
                .ToListAsync();
            
            return new PaginatedList<PostDTO>(posts, page, size, totalPosts);
        }
    }


    
    
    
    
    
    
    
    
    
    

