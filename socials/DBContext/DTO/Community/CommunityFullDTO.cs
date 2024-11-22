using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.DTO.Community;

public class CommunityFullDTO
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreateTime { get; set; }

    [Required]
    [MinLength(1)]
    public string Name { get; set; }

    public string? Description { get; set; }

    public bool IsClosed { get; set; } = false;

    public int SubscribersCount { get; set; } = 0;
    
    public List<CommunityUserDTO> Administrators { get; set; } = new List<CommunityUserDTO>();
}