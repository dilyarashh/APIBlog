namespace socials.DBContext.Models;
public class PostTags
{
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}