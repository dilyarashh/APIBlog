using socials.DBContext.DTO.Community;
using socials.DBContext.DTO.Post;
using socials.DBContext.Models.Enums;

namespace socials.Services.IServices;

public interface ICommunityService
{
    Task<List<CommunityDTO>> GetCommunityList();
    Task<List<CommunityUserDTO>> GetUserCommunity(string? token);
    Task<CommunityFullDTO?> GetInformationAboutCommunity(Guid id);
    Task<CommunityRole?> GetUserRoleInCommunity(Guid communityId, string? token);
    Task<Guid> CreatePostForCommunity(CreatePostDTO post, string? token, Guid communityId);
    Task SubscribeToCommunity(Guid communityId, string? token);
    Task UnsubscribeFromCommunity(Guid communityId, string? token);
}