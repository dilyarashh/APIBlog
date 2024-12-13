using Microsoft.AspNetCore.Mvc;
using socials.Services.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace socials.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController(IAdressService addressService) : ControllerBase
{
    [HttpGet]
    [Route("search")]
    [SwaggerOperation(Summary = "Поиск адреса по родительскому объекту или символам")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные получены", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> Search(long parentObjectId, string? query)
    {
        var list = await addressService.Search(parentObjectId, query);
        return Ok(list);
    }

    [HttpGet]
    [Route("chain")]
    [SwaggerOperation(Summary = "Получение цепочки адреса")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные получены", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> Chain(Guid objectGuid)
    {
        var list = await addressService.Chain(objectGuid);
        return Ok(list);
    }
}