using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.DTO.Post;
public class PostDTO
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; }
    [Required]
    public String Title { get; set; }
    [Required]
    public String Description { get; set; }
    [Required]
    public int ReadingTime { get; set; }
    [Url]
    public string Image { get; set; }
    [Required]
    public Guid AuthorId { get; set; }
    [Required]
    public string Author { get; set; }
    public Guid? CommunityId { get; set; }
    public string? CommunityName { get; set; }
    public Guid? AddressId { get; set; }
    [Required]
    public int Likes { get; set; }
    [Required]
    public int CommentsCount { get; set; }
    public List<Guid> Tags { get; set; }
}