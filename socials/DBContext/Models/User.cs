using System.ComponentModel.DataAnnotations;
using socials.DBContext.Models.Enums;

namespace socials.DBContext.Models;
public class User
{
    [Required]
    public Guid Id { get; init; }
    [Required]
    public DateTime CreateTime { get; init; }
    [Required]
    public  string Name { get; set; }
    [Required]
    public string Password { get; init; }
    [Required]
    public DateTime Birthday { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Email { get; set; }
    
    public virtual ICollection<CommunityUser> Communities { get; set; } = new HashSet<CommunityUser>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    
}