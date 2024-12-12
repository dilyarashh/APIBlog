namespace socials.DBContext.Email;

public class Subscriber
{
    public int Id { get; set; }
    public string Email { get; set; } = null!; 
    public bool IsActive { get; set; } = true; 
    public DateTime? LastNotificationSent { get; set; } 
}