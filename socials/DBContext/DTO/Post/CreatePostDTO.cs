using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.DTO.Post;
public class CreatePostDTO
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int ReadingTime { get; set; }
    public string Image { get; set; }
    public Guid AddressId { get; set; }
    [Required]
    public List<Guid> Tags { get; set; }
}