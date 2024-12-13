using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.Models;
public sealed class Community
{
    [Required]
    public Guid Id { get; init; }
    [Required]
    public DateTime CreateTime { get; init; }
    [Required]
    public string Name { get; init; }
    public string? Description { get; init; }
    [Required]
    public bool IsClosed { get; init; } 
    [Required]
    public int SubscribersCount { get; set; } 
    public ICollection<CommunityUser> CommunityUsers { get; set; } = new HashSet<CommunityUser>();
}