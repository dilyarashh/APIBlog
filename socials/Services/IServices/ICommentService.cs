using socials.DBContext.DTO.Comment;
using socials.DBContext.Models;

namespace socials.Services.IServices;

public interface ICommentService
{
    Task<CommentDTO> AddComment(Guid postId, CreateCommentDTO createCommentDto, string token);
    Task UpdateComment(Guid commentId, EditCommentDTO comment, string token);
    Task DeleteComment(Guid commentId, string token);
    Task<CommentChain> GetCommentChain(Guid commentId);
}