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
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly TokenInteractions _tokenService;
    public UserController(IUserService userService, TokenInteractions tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Авторизация пользователя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Успешная авторизация", typeof(TokenDTO))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка авторизации", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var tokenResponse = await _userService.Login(loginDto);
        return Ok(tokenResponse); 
    }

    [HttpPost("register")]
    [SwaggerOperation(
        Summary = "Регистрация нового пользователя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Успешная регистрация", typeof(TokenDTO))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибки валидации", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationDto)
    {

        var tokenResponse = await _userService.Register(registrationDto);
        return Ok(tokenResponse);
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpGet("profile")]
    [SwaggerOperation(
        Summary = "Получение профиля авторизованного пользователя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные получены", typeof(UserDTO))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> GetProfile()
    {
        string? token = _tokenService.GetTokenFromHeader();
        return Ok(await _userService.GetProfile(token));
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPut("edit")]
    [SwaggerOperation(
        Summary = "Редактирование профиля авторизованного пользователя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные изменены")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибки валидации", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> EditProfile([FromBody] EditDTO editDto)
    {
        string? token = _tokenService.GetTokenFromHeader();
        await _userService.EditProfile(token, editDto);
        return Ok();
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpGet("logout")]
    [SwaggerOperation(
        Summary = "Выход из профиля")]
    [SwaggerResponse(StatusCodes.Status200OK, "Выход осуществлен")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> Logout()
    {
        string? token = _tokenService.GetTokenFromHeader();
        await _userService.Logout(token);
        return Ok();
    }
}
