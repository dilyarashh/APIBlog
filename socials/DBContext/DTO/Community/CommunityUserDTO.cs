using System.ComponentModel.DataAnnotations;
using socials.DBContext.Models.Enums;

namespace socials.DBContext.DTO.Community;

public class CommunityUserDTO
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid CommunityId { get; set; }
    [Required]
    public CommunityRole Role { get; set; }

}