using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using socials.DBContext.DTO.User;
using socials.Services.IServices;
using socials.SupportiveServices.Token;

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
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var tokenResponse = await _userService.Login(loginDto);
        return Ok(tokenResponse); 
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationDto)
    {

        var tokenResponse = await _userService.Register(registrationDto);
        return Ok(tokenResponse);
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        string token = _tokenService.GetTokenFromHeader();

        return Ok(await _userService.GetProfile(token));
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPut("edit")]
    public async Task<IActionResult> EditProfile([FromBody] EditDTO editDto)
    {
        string token = _tokenService.GetTokenFromHeader();

        await _userService.EditProfile(token, editDto);
        return Ok();
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        string token = _tokenService.GetTokenFromHeader();

        await _userService.Logout(token);
        return Ok();

    }
}
