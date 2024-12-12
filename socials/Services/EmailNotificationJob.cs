using socials.DBContext;
using socials.DBContext.Models;

namespace socials.Services;

using Quartz;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using socials.Services.IServices; // Assuming this is where IEmailService is defined
using Microsoft.EntityFrameworkCore; //  For DbContext

public class EmailNotificationJob : IJob
{
    private readonly IEmailService _emailService;
    private readonly AppDbcontext _context; // Add DbContext
    private readonly ISchedulerFactory _schedulerFactory; // Add SchedulerFactory
    private readonly ILogger<EmailNotificationJob> _logger;

    public EmailNotificationJob(
        IEmailService emailService,
        AppDbcontext context, // Inject DbContext
        ISchedulerFactory schedulerFactory, // Inject SchedulerFactory
        ILogger<EmailNotificationJob> logger)
    {
        _emailService = emailService;
        _context = context;
        _schedulerFactory = schedulerFactory;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
{
    var jobDataMap = context.JobDetail.JobDataMap;
    var postId = jobDataMap.GetGuid("postId");
    var subscriberEmail = jobDataMap.GetString("subscriberEmail");

    _logger.LogInformation("EmailNotificationJob: Executing for postId: {PostId}, subscriberEmail: {SubscriberEmail}", postId, subscriberEmail);

    if (string.IsNullOrEmpty(subscriberEmail) || postId == Guid.Empty)
    {
        _logger.LogError("EmailNotificationJob: Missing data in JobDataMap!");
        return;
    }

    Post post = null;
    try
    {
        post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId); // Используем FirstOrDefaultAsync
        if (post == null)
        {
            _logger.LogError("EmailNotificationJob: Post not found. PostId: {PostId}", postId);
            return;
        }
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "EmailNotificationJob: Error retrieving post from database.");
        // Здесь можно добавить логику повторной попытки или уведомления
        return;
    }

    for (int attempt = 1; attempt <= 3; attempt++) // Попытки отправки
    {
        try
        {
            await _emailService.SendEmailAsync(subscriberEmail, "Новый пост!", $"Новый пост '{post.Title}' опубликован в сообществе '{post.CommunityName}'");
            _logger.LogInformation("EmailNotificationJob: Email sent successfully to {Email} (attempt {Attempt})", subscriberEmail, attempt);
            return; // Успешно отправлено
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "EmailNotificationJob: Error sending email notification to {Email} (attempt {Attempt})", subscriberEmail, attempt);
            await Task.Delay(TimeSpan.FromSeconds(5 * attempt)); // Пауза между попытками
        }
    }

    _logger.LogError("EmailNotificationJob: Failed to send email to {Email} after multiple attempts.", subscriberEmail);
    // Можно сохранить информацию об ошибке в базу данных
}
}
