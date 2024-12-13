namespace socials.DBContext.Models;
public class PostLike
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }

    public virtual User User { get; set; } 
    public virtual Post Post { get; set; } 
}