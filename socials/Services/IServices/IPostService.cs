using socials.DBContext.DTO.Post;

namespace socials.Services.IServices;
public interface IPostService
{
    Task<Guid> CreatePost(CreatePostDTO post, string? token);
    Task<PostDTO> GetPostById(Guid id);
    Task<bool> AddLikeToPost(Guid postId, Guid userId);
    Task<bool> DeleteLikeFromPost(Guid postId, Guid userId);
}