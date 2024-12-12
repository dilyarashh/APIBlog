using System.Net.Mail;
using socials.Services.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace socials.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        _logger.LogInformation("EmailService: Sending email to {to}", to);
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
                _logger.LogInformation("EmailService: Email sent successfully to {to}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EmailService: Error sending email to {to}", to);
                throw;
            }
        }
    }
}