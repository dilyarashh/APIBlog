namespace socials.DBContext.Email;

public class EmailMessage
{
    public int Id { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public int RetryCount { get; set; } = 0; // Счетчик попыток
    public DateTime? LastAttempt { get; set; } // Время последней попытки
    public bool Sent { get; set; } = false; // Флаг успешной отправки
}