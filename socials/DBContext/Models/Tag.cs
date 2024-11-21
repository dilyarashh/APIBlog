using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.Models;

public class Tag
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; }
    [Required]
    public string Name { get; set; }
}