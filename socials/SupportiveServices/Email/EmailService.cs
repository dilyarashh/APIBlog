using System.Net.Mail;
using socials.Services.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using socials.DBContext;
using socials.DBContext.Models;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace socials.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;
    private readonly AppDbcontext _context; // Ваш контекст базы данных

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger, AppDbcontext context)
    {
        _configuration = configuration;
        _logger = logger;
        _context = context;
    }

    public async Task SendEmailAsync(string to, string subject, string body, Guid postId, Guid userId)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_configuration["Email:EmailFrom"], _configuration["Email:EmailFrom"]));
        message.To.Add(new MailboxAddress(to, to));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body }; 

        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_configuration["Email:SmtpServer"], int.Parse(_configuration["Email:SmtpPort"]), SecureSocketOptions.None);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                _logger.LogInformation("Email sent successfully to {to}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {to}. Adding to queue.", to);
                await AddEmailToQueueAsync(postId, userId, to, subject, body); 
            }
        }
    }

    private async Task AddEmailToQueueAsync(Guid postId, Guid userId, string email, string subject, string body)
    {
        var emailQueue = new EmailQueues
        {
            PostId = postId,
            UserId = userId,
            Email = email,
            Subject = subject,
            Body = body,
            IsDelivered = false,
            CreatedAt = DateTime.UtcNow
        };

        await _context.EmailQueues.AddAsync(emailQueue);
        await _context.SaveChangesAsync();
    }
}