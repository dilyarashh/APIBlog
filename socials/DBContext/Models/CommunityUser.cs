using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using socials.DBContext.Models.Enums;

namespace socials.DBContext.Models;

public class CommunityUser
{
    public Guid CommunityId { get; set; }
    public Guid UserId { get; set; }
    public CommunityRole Role { get; set; }

    [ForeignKey("CommunityId")]
    public virtual Community Community { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}