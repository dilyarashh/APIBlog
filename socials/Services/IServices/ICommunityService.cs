using socials.DBContext.DTO.Community;
using socials.DBContext.Models.Enums;

namespace socials.Services.IServices;

public interface ICommunityService
{
    Task<CommunityFullDTO?> GetCommunity(Guid id);
    
    Task<List<CommunityUserDTO>> GetUserCommunity(string token);
    
    Task<List<CommunityDTO>> GetCommunityList();
    
    Task<CommunityRole?> GetUserRoleAsync(Guid communityId, string token);
    
    Task Subscribe(Guid communityId, string token);
    
    Task Unsubscribe(Guid communityId, string token);
    
}