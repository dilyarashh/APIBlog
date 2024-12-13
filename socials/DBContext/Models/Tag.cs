namespace socials.DBContext.Models;
public class Tag
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string Name { get; set; }
    public ICollection<PostTags> PostTags { get; set; } = new List<PostTags>();
}