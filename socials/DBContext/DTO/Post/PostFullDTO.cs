using System.ComponentModel.DataAnnotations;
using socials.DBContext.DTO.Tag;
namespace socials.DBContext.DTO.Post;
public class PostFullDTO
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
    public String Image { get; set; }
    [Required]
    public Guid AuthorId { get; set; }
    [Required]
    public String Author { get; set; }
    public Guid? CommunityId { get; set; }
    public String? CommunityName { get; set; }
    public Guid? AddressId { get; set; }
    [Required]
    public int Likes { get; set; }
    [Required]
    public bool HasLike { get; set; }
    [Required]
    public int CommentsCount { get; set; }
    public List<TagDTO> Tags { get; set; }
    public List<Models.Comment> Comments { get; set; } = new List<Models.Comment>();
}