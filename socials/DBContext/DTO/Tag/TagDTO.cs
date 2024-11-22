using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.DTO.Tag;

public class TagDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; }
    [Required]
    public string Name { get; set; }
    
}