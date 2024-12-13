using Microsoft.AspNetCore.Mvc;
using socials.DBContext.DTO.Tag;
using socials.DBContext.Models;
using socials.Services.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace socials.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController(ITagService tagService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Просмотр списка тегов")]
    [SwaggerResponse(StatusCodes.Status200OK, "Список получен", typeof(TagDTO))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> GetTags()
    {
        var tags = await tagService.GetTags();
        return Ok(tags);
    }
}