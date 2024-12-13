using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using socials.DBContext.DTO.User;
using socials.DBContext.Models;
using socials.Services.IServices;
using socials.SupportiveServices.Token;
using Swashbuckle.AspNetCore.Annotations;

namespace socials.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService, TokenInteractions tokenService) : Controller
{
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Авторизация пользователя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Успешная авторизация", typeof(TokenDTO))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка авторизации", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var tokenResponse = await userService.Login(loginDto);
        return Ok(tokenResponse); 
    }

    [HttpPost("register")]
    [SwaggerOperation(Summary = "Регистрация нового пользователя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Успешная регистрация", typeof(TokenDTO))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибки валидации", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationDto)
    {

        var tokenResponse = await userService.Register(registrationDto);
        return Ok(tokenResponse);
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpGet("profile")]
    [SwaggerOperation(Summary = "Получение профиля авторизованного пользователя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные получены", typeof(UserDTO))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> GetProfile()
    {
        var token = tokenService.GetTokenFromHeader();
        return Ok(await userService.GetProfile(token));
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPut("edit")]
    [SwaggerOperation(Summary = "Редактирование профиля авторизованного пользователя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные изменены")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибки валидации", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> EditProfile([FromBody] EditDTO editDto)
    {
        var token = tokenService.GetTokenFromHeader();
        await userService.EditProfile(token, editDto);
        return Ok();
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpGet("logout")]
    [SwaggerOperation(Summary = "Выход из профиля")]
    [SwaggerResponse(StatusCodes.Status200OK, "Выход осуществлен")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> Logout()
    {
        var token = tokenService.GetTokenFromHeader();
        await userService.Logout(token);
        return Ok();
    }
}
