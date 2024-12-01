using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.DTO.Community;

public class CommunityDTO
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
}