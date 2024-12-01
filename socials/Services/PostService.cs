using Microsoft.EntityFrameworkCore;
using socials.DBContext;
using socials.DBContext.DTO.Post;
using socials.DBContext.DTO.Tag;
using socials.DBContext.Models;
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
    
    public async Task<PostDTO> GetPostById(Guid id)
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

        return new PostDTO
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
            var isMember = await context.CommunityUsers.AnyAsync(cu => cu.CommunityId == post.CommunityId && cu.UserId == userId);
            if (!isMember)
            {
                throw new BadRequestException("Для того, чтобы поставить лайк на пост закрытой группы, нужно быть её подписчиком");
            }
        }
        
        context.PostLikes.Add(new PostLike { PostId = postId, UserId = userId });
        await context.SaveChangesAsync();

        post.Likes++;
        if (post.Likes > 0) { post.HasLike = true; }
        await context.SaveChangesAsync(); 

        return true;
    }
    
    public async Task<bool> DeleteLikeFromPost(Guid postId, Guid userId) 
    {
        var existingLike = await context.PostLikes.FirstOrDefaultAsync(pl => pl.PostId == postId && pl.UserId == userId);

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
        if (post.Likes == 0) { post.HasLike = false; }
        await context.SaveChangesAsync();
        
        return true;
    }
}
