using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using socials.DBContext.DTO.Community;
using socials.DBContext.DTO.Post;
using socials.DBContext.Models;
using socials.DBContext.Models.Enums;
using socials.Services.IServices;
using socials.SupportiveServices.Token;
using Swashbuckle.AspNetCore.Annotations;

namespace socials.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommunitiesController(ICommunityService communityService, TokenInteractions tokenService)
    : ControllerBase
{
    [HttpPost("create_community")]
    [SwaggerOperation(Summary = "Создать сообщество")]
    [SwaggerResponse(StatusCodes.Status200OK, "Сообщество создано", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> CreateCommunity([FromBody] CreateCommunityDTO communityDTO)
    {
        var token = tokenService.GetTokenFromHeader(); 
        var communityId = await communityService.CreateCommunity(communityDTO, token);
        return Ok(communityId);
    }
    
    [HttpGet("list")]
    [SwaggerOperation(Summary = "Получение списка всех сообществ")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные получены", typeof(CommunityDTO))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> GetCommunityList()
    {
        var communities = await communityService.GetCommunityList();
        return Ok(communities);
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpGet("my")]
    [SwaggerOperation(Summary = "Получение сообществ авторизованного пользователя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные получены", typeof(CommunityUserDTO))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> GetUserCommunity()
    {
        var token = tokenService.GetTokenFromHeader(); 
        var communities = await communityService.GetUserCommunity(token);
        return Ok(communities);
    }
    
    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Получение информации о конкретном сообществе")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные получены", typeof(CommunityFullDTO))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Сообщество не найдено")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> GetCommunity(Guid id)
    {
        var community = await communityService.GetInformationAboutCommunity(id);
        return Ok(community);
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpGet("{communityId:guid}/role")]
    [SwaggerOperation(Summary = "Получение информации о роли авторизованного пользователя в конкретном сообществе")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные получены", typeof(CommunityRole))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Сообщество не найдено")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> GetUserRoleInCommunity(Guid communityId)
    {
        var token = tokenService.GetTokenFromHeader();
        var communityRole = await communityService.GetUserRoleInCommunity(communityId, token);
        return Ok(communityRole);
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPost("{communityId:guid}/post")] 
    [SwaggerOperation(Summary = "Создание поста в конкретном сообществе")]
    [SwaggerResponse(StatusCodes.Status200OK, "Пост опубликован", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибки валидации", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> CreatePost(Guid communityId, [FromBody] CreatePostDTO post) 
    {
        var token = tokenService.GetTokenFromHeader();
        var postId = await communityService.CreatePostForCommunity(post, token, communityId);
        return Ok(postId);
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPost("{communityId}/subscribe")]
    [SwaggerOperation(Summary = "Подписаться на сообщество")]
    [SwaggerResponse(StatusCodes.Status200OK, "Подписка осуществлена", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Пользователь уже подписан на сообщество", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> SubscribeToCommunity(Guid communityId) 
    {
        var token = tokenService.GetTokenFromHeader(); 
        await communityService.SubscribeToCommunity(communityId, token);
        return Ok();
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPost("{communityId}/unsubscribe")]
    [SwaggerOperation(Summary = "Отписаться от сообщества")]
    [SwaggerResponse(StatusCodes.Status200OK, "Отписка осуществлена", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Пользователь не был подписан на сообщество", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> UnsubscribeFromCommunity(Guid communityId) 
    {
        var token = tokenService.GetTokenFromHeader(); 
        await communityService.UnsubscribeFromCommunity(communityId, token);
        return Ok();
    }
    
    [HttpGet("{id}/list posts")]
    [SwaggerOperation(Summary = "Получить посты определенного сообщества")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные получены", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера")]
    public async Task<IActionResult> GetCommunityPostsAsync(
        [FromRoute] Guid id, 
        [FromQuery] string[]? tags,
        [FromQuery] SortingOrder sorting = SortingOrder.CreateDesc, 
        [FromQuery] int page = 1,
        [FromQuery] int size = 5)
    {
        var posts = await communityService.GetCommunityPosts(id, tags, sorting, page, size);
        return Ok(posts);
    }
}