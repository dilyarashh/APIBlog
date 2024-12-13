using System;
using System.Threading.Tasks;

namespace socials.Services.IServices
{
    public interface IEmailService
    {
        /// <summary>
        /// Отправляет электронное письмо указанному получателю.
        /// </summary>
        /// <param name="to">Адрес электронной почты получателя.</param>
        /// <param name="subject">Тема письма.</param>
        /// <param name="body">Содержимое письма.</param>
        /// <param name="postId">Идентификатор поста (если применимо).</param>
        /// <param name="userId">Идентификатор пользователя (если применимо).</param>
        /// <returns>Асинхронная задача.</returns>
        Task SendEmailAsync(string to, string subject, string body, Guid postId, Guid userId);
    }
}