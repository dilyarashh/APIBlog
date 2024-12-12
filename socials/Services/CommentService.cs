using Microsoft.EntityFrameworkCore;
using socials.DBContext;
using socials.DBContext.DTO.Comment;
using socials.DBContext.Models;
using socials.Services.IServices;
using socials.SupportiveServices.Exceptions;
using socials.SupportiveServices.Token;
using socials.SupportiveServices.Validations;

namespace socials.Services;

public class CommentService(AppDbcontext context, TokenInteractions tokenService) : ICommentService
{

    public async Task<CommentDTO> AddComment(Guid postId, CreateCommentDTO createCommentDto, string token)
    {
        var userId = tokenService.GetIdFromToken(token);
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedException("Пользователь не авторизован");
        }

        var user = await context.Users.FirstOrDefaultAsync(d => d.Id == Guid.Parse(userId));
        
        var post = await context.Posts.FindAsync(postId);
        if (post == null)
        {
            throw new NotFoundException("Пост не найден");
        }
        
        var community = await context.Communities.FindAsync(post.CommunityId); 
        if (community.IsClosed)
        {
            var isMember = await context.CommunityUsers.AnyAsync(cu => cu.CommunityId == community.Id && cu.UserId == user.Id);
            if (!isMember)
            {
                throw new UnauthorizedException("Чтобы оставить комментарий к посту закрытого сообщества, нужно быть его участником");
            }
        }

        if (createCommentDto.ParentId.HasValue)
        {
            var parentComment = await context.Comments.FindAsync(createCommentDto.ParentId.Value);
            if (parentComment == null)
            {
                throw new NotFoundException("Родительский комментарий не найден.");
            }
        }
        
        if (!CommentValidator.ValidateComment(createCommentDto.Content)) 
        {
            throw new BadRequestException("Длина комментария должна быть от 1 до 100 символов");
        }
        
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            CreateTime = DateTime.UtcNow,
            Content = createCommentDto.Content,
            AuthorId = user.Id,
            Author = user.Name, 
            SubComments = 0,
            ParentId = createCommentDto.ParentId,
            PostId = postId,
            AuthorUser = user 
        };

        context.Comments.Add(comment);
        await context.SaveChangesAsync();

        if (createCommentDto.ParentId.HasValue)
        {
            var parentComment = await context.Comments.FindAsync(createCommentDto.ParentId.Value);
            if (parentComment != null)
            {
                parentComment.SubComments++;
                context.Entry(parentComment).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        var commentDto = new CommentDTO
        {
            Id = comment.Id,
            CreateTime = comment.CreateTime,
            Content = comment.Content,
            AuthorId = comment.AuthorId,
            Author = comment.Author, 
            SubComments = comment.SubComments,
            ParentId = comment.ParentId,
            PostId = comment.PostId
        };

        return commentDto;
    }
    
    public async Task UpdateComment(Guid commentId, EditCommentDTO comment, string token)
    {
        var userId = tokenService.GetIdFromToken(token);
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedException("Пользователь не авторизован");
        }

        var commentFromDb = await context.Comments.FindAsync(commentId);
        if (commentFromDb == null)
        {
            throw new NotFoundException("Комментарий не найден");
        }

        if (commentFromDb.AuthorId != Guid.Parse(userId))
        {
            throw new UnauthorizedException("Редактировать комментарий может только его автор");
        }

        if (!CommentValidator.ValidateComment(comment.Content))
        {
            throw new BadRequestException("Длина комментария должна быть от 1 до 100 символов");
        }
        
        commentFromDb.Content = comment.Content;
        commentFromDb.ModifiedDate = DateTime.UtcNow;

        context.Entry(commentFromDb).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteComment(Guid commentId, string token)
    {
        var comment = await context.Comments.FindAsync(commentId);
        if (comment == null)
        {
            throw new NotFoundException("Комментарий не найден.");
        }

        var userId = tokenService.GetIdFromToken(token);
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedException("Пользователь не авторизован.");
        }

        if (comment.AuthorId != Guid.Parse(userId))
        {
            throw new UnauthorizedException("У вас нет прав для удаления этого комментария.");
        }
        
        var parentComment = await context.Comments.FindAsync(comment.ParentId);
        
        if (comment.SubComments > 0)
        {
            comment.Content = "";
            comment.DeleteDate = DateTime.UtcNow;
        }
        else
        {
            context.Comments.Remove(comment);
        }
        
        if (parentComment != null)
        {
            parentComment.SubComments--;
            context.Entry(parentComment).State = EntityState.Modified;
        }
        
        await context.SaveChangesAsync();
    }
    
    public async Task<CommentChain> GetCommentChain(Guid commentId)
    {
        var comment = await context.Comments
            .Include(c => c.ParentComment)
            .FirstOrDefaultAsync(c => c.Id == commentId);

        if (comment == null || comment.DeleteDate != null)
        {
            throw new NotFoundException("Comment not found or deleted.");
        }

        await LoadAllDescendants(comment);
        return BuildCommentChain(comment);
    }

    private async Task LoadAllDescendants(Comment comment)
    {
        if (comment == null) return;

        comment.SubCommentsList = await context.Comments
            .Where(c => c.ParentId == comment.Id)
            .Include(c => c.SubCommentsList) 
            .ToListAsync();

        foreach (var child in comment.SubCommentsList)
        {
            await LoadAllDescendants(child);
        }
    }

    private CommentChain BuildCommentChain(Comment comment)
    {
        var chain = new CommentChain();
        PopulateCommentChain(comment, chain);
        return chain;
    }

    private void PopulateCommentChain(Comment comment, CommentChain chain)
    {
        if (comment == null) return;

        AddAncestors(comment, chain); 
        AddDescendants(comment, chain); 
        if (!chain.Comments.Any(c => c.Id == comment.Id)) { 
            chain.Comments.Add(MapToCommentDTO(comment));
        }
    }

    private void AddAncestors(Comment comment, CommentChain chain)
    {
        HashSet<Guid> visited = new HashSet<Guid>();
        AddAncestorsRecursive(comment, chain, visited);
    }
    private void AddAncestorsRecursive(Comment comment, CommentChain chain, HashSet<Guid> visited)
    {
        if (comment == null || visited.Contains(comment.Id))
            return;

        visited.Add(comment.Id);
        chain.Comments.Insert(0, MapToCommentDTO(comment)); 
        if (comment.ParentId != null)
        {
            var parent = context.Comments.FirstOrDefault(c => c.Id == comment.ParentId);
            AddAncestorsRecursive(parent, chain, visited);
        }
    }

    private void AddDescendants(Comment comment, CommentChain chain)
    {
        AddDescendantsRecursive(comment, chain, new HashSet<Guid>());
    }

    private void AddDescendantsRecursive(Comment comment, CommentChain chain, HashSet<Guid> visited)
    {
        if (comment == null || visited.Contains(comment.Id)) return;

        visited.Add(comment.Id);
        foreach (var child in comment.SubCommentsList)
        {
            chain.Comments.Add(MapToCommentDTO(child));
            AddDescendantsRecursive(child, chain, visited);
        }
    }
    private CommentDTO MapToCommentDTO(Comment comment)
    {
        return new CommentDTO
        {
            Id = comment.Id,
            CreateTime = comment.CreateTime,
            Content = comment.Content,
            AuthorId = comment.AuthorId,
            Author = comment.Author,
            SubComments = comment.SubComments,
            ParentId = comment.ParentId,
            PostId = comment.PostId
        };
    }
}
