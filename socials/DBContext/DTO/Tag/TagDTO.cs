using System.ComponentModel.DataAnnotations;
using socials.DBContext.Models;

namespace socials.DBContext.DTO.Tag;

public class TagDTO
{
    [Key]
    public Guid Id { get; init; }
    [Required]
    public DateTime CreateTime { get; init; }
    [Required]
    public string Name { get; init; }
    
    public virtual ICollection<PostTags> PostTags { get; set; } = new List<PostTags>();
    
}