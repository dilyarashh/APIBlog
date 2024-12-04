using socials.DBContext.DTO.Post;
using socials.DBContext.Models;
using socials.DBContext.Models.Enums;

namespace socials.Services.IServices;
public interface IPostService
{
    Task<Guid> CreatePost(CreatePostDTO post, string? token);
    Task<PostFullDTO> GetPostById(Guid id);
    Task<bool> AddLikeToPost(Guid postId, Guid userId);
    Task<bool> DeleteLikeFromPost(Guid postId, Guid userId);
    Task<PaginatedList<PostDTO>> GetPosts(
        string? token,
        string[]? tags,
        string? author,
        int? min,
        int? max,
        SortingOrder sortingOrder,
        bool onlyMyCommunities,
        int page = 1,
        int size = 5);
}