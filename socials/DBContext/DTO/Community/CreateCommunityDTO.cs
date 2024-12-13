using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.DTO.Community;
public class CreateCommunityDTO
{
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    public bool IsClosed { get; set; }
}