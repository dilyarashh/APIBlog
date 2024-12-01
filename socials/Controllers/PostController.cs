using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using socials.DBContext.DTO.Post;
using socials.DBContext.Models;
using socials.Services.IServices;
using socials.SupportiveServices.Token;
using Swashbuckle.AspNetCore.Annotations;

namespace socials.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PostsController(IPostService postService, TokenInteractions tokenService) : ControllerBase
{
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPost]
    [SwaggerOperation(Summary = "Создание персонального поста")]
    [SwaggerResponse(StatusCodes.Status200OK, "Пост опубликован", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибки валидации", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO post) 
    {
        var token = tokenService.GetTokenFromHeader();
        var postId = await postService.CreatePost(post, token);
        return Ok(new { PostId = postId }); 
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Получение информации о конкретном посте")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные получены", typeof(PostDTO))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> GetPostById(Guid id)
    {
        var postDto = await postService.GetPostById(id);
        return Ok(postDto);
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPost("{postId}/like")]
    [SwaggerOperation(Summary = "Поставить лайк на конкретный пост")]
    [SwaggerResponse(StatusCodes.Status200OK, "Лайк поставлен")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> AddLikeToPost(Guid postId)
    {
        var token = tokenService.GetTokenFromHeader();
        var userId = tokenService.GetIdFromToken(token);
        var result = await postService.AddLikeToPost(postId, Guid.Parse(userId));
        return Ok(result);
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPost("{postId}/dislike")]
    [SwaggerOperation(Summary = "Убрать лайк с конкретного поста")]
    [SwaggerResponse(StatusCodes.Status200OK, "Лайк поставлен")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> DeleteLikeToPost(Guid postId)
    {
        var token = tokenService.GetTokenFromHeader();
        var userId = tokenService.GetIdFromToken(token);
        var result = await postService.DeleteLikeFromPost(postId, Guid.Parse(userId));
        return Ok(result);
    }
}