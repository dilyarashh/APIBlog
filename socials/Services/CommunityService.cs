using Microsoft.EntityFrameworkCore;
using socials.DBContext;
using socials.DBContext.DTO.Community;
using socials.DBContext.DTO.Post;
using socials.DBContext.DTO.User;
using socials.DBContext.Models;
using socials.DBContext.Models.Enums;
using socials.Services.IServices;
using socials.SupportiveServices.Exceptions;
using socials.SupportiveServices.Token;
using socials.SupportiveServices.Validations;

namespace socials.Services;

public class CommunityService(AppDbcontext context, TokenInteractions tokenService, GARContext garContext)
    : ICommunityService
{
    public async Task<List<CommunityDTO>> GetCommunityList()
    {
        return await context.Communities 
            .Select(community => new CommunityDTO
            {
                Id = community.Id,
                CreateTime = community.CreateTime,
                Name = community.Name,
                Description = community.Description,
                IsClosed = community.IsClosed,
                SubscribersCount = community.SubscribersCount,
            })
            .ToListAsync();
    }
    public async Task<List<CommunityUserDTO>> GetUserCommunity(string? token)
    {
        var userIdString = tokenService.GetIdFromToken(token);
        if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out var userId))
            return await context.CommunityUsers
                .Where(cu => cu.UserId == userId)
                .Select(cu => new CommunityUserDTO
                {
                    UserId = cu.UserId,
                    CommunityId = cu.CommunityId,
                    Role = cu.Role == 0 ? CommunityRole.Administrator : CommunityRole.Subscriber
                })
                .ToListAsync();
        throw new UnauthorizedException("Пользователь не авторизован");
    }
    public async Task<CommunityFullDTO?> GetInformationAboutCommunity(Guid id)
    {
        var community = await context.Communities
            .Include(c => c.CommunityUsers) 
            .ThenInclude(cu => cu.User) 
            .FirstOrDefaultAsync(c => c.Id == id);
        if (community == null)
        {
            throw new NotFoundException("Сообщество не найдено");
        }
        
        var communityFullDto = new CommunityFullDTO
        {
            Id = community.Id,
            CreateTime = community.CreateTime,
            Name = community.Name,
            Description = community.Description,
            IsClosed = community.IsClosed,
            SubscribersCount = community.SubscribersCount,
            Administrators = community.CommunityUsers
                .Where(cu => cu.Role == CommunityRole.Administrator)
                .Select(cu => new UserDTO
                {
                    Id = cu.UserId,
                    CreateTime = cu.User.CreateTime,
                    Name = cu.User.Name,
                    Birthday = cu.User.Birthday,
                    Gender = cu.User.Gender,
                    Email = cu.User.Email,
                    Phone = cu.User.Phone
                })
                .ToList()
        };
        return communityFullDto;
    }
    public async Task<CommunityRole?> GetUserRoleInCommunity(Guid communityId, string? token)
    {
        var userId = Guid.Parse(tokenService.GetIdFromToken(token));
        if (userId == Guid.Empty)
        {
            throw new UnauthorizedException("Пользователь не авторизован");
        }

        var community = await context.Communities.FirstOrDefaultAsync(c => c.Id == communityId);
        if (community == null)
        {
            throw new NotFoundException("Сообщество не найдено"); 
        }

        var communityUser = await context.CommunityUsers
            .FirstOrDefaultAsync(cu => cu.UserId == userId && cu.CommunityId == communityId);
        if (communityUser == null)
        {
            throw new NotFoundException("Пользователь не подписан на сообщество"); 
        }

        return communityUser.Role;
    }
    public async Task<Guid> CreatePostForCommunity(CreatePostDTO post, string? token, Guid communityId) 
    {
        var userId = tokenService.GetIdFromToken(token);
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedException("Пользователь не авторизован");
        }
        var user = await context.Users.FirstOrDefaultAsync(d => d.Id == Guid.Parse(userId));
        
        var isCommunityAdmin = await context.CommunityUsers
            .AnyAsync(cu => cu.UserId == user.Id && cu.CommunityId == communityId && cu.Role==0);
        if (!isCommunityAdmin)
        {
            throw new UnauthorizedException("Чтобы создать пост, нужно быть администратором сообщества");
        }

        var address = await garContext.AsAddrObjs.FirstOrDefaultAsync(a => a.Objectguid == post.AddressId);
        if (address == null)
        {
            var house = await garContext.AsHouses.FirstOrDefaultAsync(h => h.Objectguid == post.AddressId);
            if (house == null)
            {
                throw new NotFoundException("Такого адреса не существует");
            }
        }
        
        var community = await context.Communities.FirstOrDefaultAsync(c => c.Id == communityId);
        if (community == null)
        {
            throw new NotFoundException("Такого сообщества не существует");
        }
        
        if(!TitleValidator.ValidateTitle(post.Title))
        {
            throw new BadRequestException("Длина заголовка должна быть от 1 до 100 символов");
        }
        
        if(!DescriptionValidator.ValidateDescription(post.Description))
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
            CommunityId = communityId,
            CommunityName = community.Name,
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
    public async Task SubscribeToCommunity(Guid communityId, string? token)
    {
        var userId = tokenService.GetIdFromToken(token);
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedException("Пользователь не авторизован");
        }
        
        var community = await context.Communities.FindAsync(communityId);
        if (community == null)
        {
            throw new NotFoundException("Такого сообщества не существует");
        }
        
        var existingSubscription = await context.CommunityUsers.FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId) && s.CommunityId == communityId);
        if (existingSubscription != null) 
        {
            throw new BadRequestException("Вы уже подписаны на это сообщество");
        }
        
        context.CommunityUsers.Add(new CommunityUser { UserId = Guid.Parse(userId), CommunityId = communityId, Role = (CommunityRole)1 });
        community.SubscribersCount++;

        await context.SaveChangesAsync();
    }
    
    public async Task UnsubscribeFromCommunity(Guid communityId, string? token)
    {
        var userId = tokenService.GetIdFromToken(token);
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedException("Пользователь не авторизован");
        }
        
        var community = await context.Communities.FindAsync(communityId);
        if (community == null)
        {
            throw new NotFoundException("Такого сообщества не существует");
        }
        
        var existingSubscription = await context.CommunityUsers
            .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId) && s.CommunityId == communityId);
        if (existingSubscription == null)
        {
            throw new BadRequestException("Вы не подписаны на это сообщество");
        }
        
        var user = await context.Users.FirstOrDefaultAsync(d => d.Id == Guid.Parse(userId));
        var isCommunityAdmin = await context.CommunityUsers
            .AnyAsync(cu => cu.UserId == user.Id && cu.CommunityId == communityId && cu.Role==0);
        if (!isCommunityAdmin)
        {
            throw new BadRequestException("Администратор не может отписаться от сообщества");
        }
        
        context.CommunityUsers.Remove(existingSubscription);
        community.SubscribersCount--;

        await context.SaveChangesAsync();
    }
    
    public async Task<PaginatedList<PostDTO>> GetCommunityPosts(Guid communityId, string[]? tags, SortingOrder sortingOrder, int page, int size)
    {
        var query = context.Posts
            .Include(p => p.Comments)
            .Include(p => p.PostTags)
            .ThenInclude(pt => pt.Tag)
            .Where(p => p.CommunityId == communityId) 
            .AsQueryable();

        if (tags != null && tags.Length > 0)
        {
            query = query.Where(p => p.PostTags.Any(pt => tags.Contains(pt.Tag.Name)));
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

        int totalPosts = await query.CountAsync();
        return new PaginatedList<PostDTO>(posts, page, size, totalPosts);
    }
}