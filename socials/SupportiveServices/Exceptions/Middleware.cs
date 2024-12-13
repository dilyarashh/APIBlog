using System.Net;
using System.Text.Json;
using socials.DBContext.Models;

namespace socials.SupportiveServices.Exceptions;

 public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext db)
        {
            try
            {
                await _next(db);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(db, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";

            if (exception is BadRequestException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : "Плохой запрос";
            }
            else if (exception is UnauthorizedException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : "Данный пользователь не авторизован";
            }
            else if (exception is NotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : "Не найдено";
            }
            response.StatusCode = statusCode;

            var error = new Error
            {
                StatusCode = statusCode.ToString(),
                Message = !string.IsNullOrEmpty(message) ? message : "Внутренняя ошибка сервера"
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(error, options);

            await response.WriteAsync(json);
        }
    }