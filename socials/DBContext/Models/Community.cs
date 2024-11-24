using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.Models;

public class Community
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
    
    public virtual ICollection<CommunityUser> CommunityUsers { get; set; } = new HashSet<CommunityUser>();
}