using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.Models;
public class EmailQueues
{
    [Key]
    public Guid Id { get; set; }
    public Guid PostId { get; set; } 
    public Guid UserId { get; set; }
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Subject { get; set; } = null!;
    [Required]
    public string Body { get; set; } = null!;
    public bool IsDelivered { get; set; } = false;
    public int Retries { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}