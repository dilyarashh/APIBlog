using System;
using System.Threading.Tasks;

namespace socials.Services.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, Guid postId, Guid userId);
    }
}