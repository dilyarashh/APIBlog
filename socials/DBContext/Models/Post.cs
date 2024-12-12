using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.Models;

public class Post
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int ReadingTime { get; set; }
    [Url]
    public string Image { get; set; }
    [Required]
    public Guid AuthorId { get; set; }
    [Required]
    public string Author { get; set; }
    public Guid? CommunityId { get; set; }
    public String? CommunityName { get; set; }
    public Guid? AddressId { get; set; }
    [Required]
    public int Likes { get; set; }
    [Required]
    public bool HasLike { get; set; }
    [Required]
    public int CommentsCount { get; set; }
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>(); 
    public ICollection<PostTags> PostTags { get; set; } = new List<PostTags>();
    public virtual Community Community { get; set; }
}