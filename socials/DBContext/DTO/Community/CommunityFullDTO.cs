using System.ComponentModel.DataAnnotations;
using socials.DBContext.DTO.User;

namespace socials.DBContext.DTO.Community;

public class CommunityFullDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public bool IsClosed { get; set; } = false;
    [Required]
    public int SubscribersCount { get; set; } = 0;
    [Required]
    public List<UserDTO> Administrators { get; set; } = [];
}