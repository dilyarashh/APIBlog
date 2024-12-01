using Microsoft.AspNetCore.Mvc;
using socials.DBContext.DTO.Author;
using socials.DBContext.Models;
using socials.Services.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace socials.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthorsController(IAuthorService authorService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Получение списка авторов")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные получены", typeof(AuthorDTO))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await authorService.GetAllAuthors();
        return Ok(authors);
    }
}