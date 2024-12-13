using Microsoft.EntityFrameworkCore;
using socials.DBContext;
using socials.Services.IServices;

namespace socials.Services;

public class EmailQueueProcessor
{
    private readonly AppDbcontext _context;
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailQueueProcessor> _logger;
    public EmailQueueProcessor(AppDbcontext context, IEmailService emailService, ILogger<EmailQueueProcessor> logger)
    {
        _context = context;
        _emailService = emailService;
        _logger = logger;
    }
    public async Task ProcessQueueAsync()
    {
        var emailQueueItems = await _context.EmailQueues
            .Where(eq => !eq.IsDelivered) 
            .ToListAsync();

        foreach (var item in emailQueueItems)
        {
            try
            {
                await _emailService.SendEmailAsync(item.Email, item.Subject, item.Body, item.PostId, item.UserId);
                item.IsDelivered = true; 
                _logger.LogInformation("Email sent from queue to {Email}", item.Email);
            }
            catch (Exception ex)
            {                item.Retries += 1; 
                _logger.LogError(ex, "Error sending email from queue to {Email}. Retry count: {Retries}", item.Email, item.Retries);
            }
        }

        await _context.SaveChangesAsync();
    }
}