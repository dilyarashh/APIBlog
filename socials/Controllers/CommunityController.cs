using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socials.DBContext.Models.Enums;
using socials.Services.IServices;
using socials.SupportiveServices.Exceptions;
using socials.SupportiveServices.Token;

namespace socials.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CommunitiesController : ControllerBase
{
    private readonly ICommunityService _communityService;
    private readonly TokenInteractions _tokenService;

    public CommunitiesController(ICommunityService communityService, TokenInteractions tokenService)
    {
        _communityService = communityService;
        _tokenService = tokenService;
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> GetCommunityList()
    {
        var communities = await _communityService.GetCommunityList();
        return Ok(communities);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommunity(Guid id)
    {
        var communityDto = await _communityService.GetCommunity(id);

        if (communityDto == null)
        {
            return NotFound();
        }

        return Ok(communityDto);
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpGet]
    public async Task<IActionResult> GetUserCommunity()
    {
        string token = _tokenService.GetTokenFromHeader(); 

        var communities = await _communityService.GetUserCommunity(token);

        return Ok(communities);
    }

    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpGet("{communityId}/role")]
    public async Task<IActionResult> GetUserRoleInCommunity(Guid communityId)
    {
        string token = _tokenService.GetTokenFromHeader();
        var result = await _communityService.GetUserRoleAsync(communityId, token);
        switch (result)
        {
            case CommunityRole role: 
                return Ok(role);
            case null: 
                return NotFound();
        }
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPost("{communityId}/subscribe")]
    public async Task<IActionResult> SubscribeToCommunity(Guid communityId) 
    {
        try
        {
            string token = _tokenService.GetTokenFromHeader(); 
            await _communityService.Subscribe(communityId, token);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return Conflict("Произошла ошибка при обновлении базы данных.  Попробуйте позже.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPost("{communityId}/unsubscribe")]
    public async Task<IActionResult> UnsubscribeToCommunity(Guid communityId) 
    {
        try
        {
            string token = _tokenService.GetTokenFromHeader(); 
            await _communityService.Unsubscribe(communityId, token);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return Conflict("Произошла ошибка при обновлении базы данных.  Попробуйте позже.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}