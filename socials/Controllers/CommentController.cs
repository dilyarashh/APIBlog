using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using socials.DBContext.DTO.Comment;
using socials.DBContext.Models;
using socials.Services.IServices;
using socials.SupportiveServices.Exceptions;
using socials.SupportiveServices.Token;
using Swashbuckle.AspNetCore.Annotations;

namespace socials.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CommentsController(ICommentService commentService, TokenInteractions tokenService) : ControllerBase
{

    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPost("comment")]
    [SwaggerOperation(Summary = "Написать комментарий к определенному посту")]
    [SwaggerResponse(StatusCodes.Status200OK, "Комментарий отправлен", typeof(CommentDTO))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибки валидации", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Пост не найден")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> AddComment(Guid postId, [FromBody] CreateCommentDTO createCommentDto)
    {
        var token = tokenService.GetTokenFromHeader(); 
        var comment = await commentService.AddComment(postId, createCommentDto, token);
        return Ok(comment);
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpPut("editcomment")]
    [SwaggerOperation(Summary = "Отредактировать комментарий")]
    [SwaggerResponse(StatusCodes.Status200OK, "Комментарий отредактирован")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибки валидации", typeof(Error))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Комментарий не найден")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> AddComment(Guid commentId, [FromBody] EditCommentDTO editCommentDto)
    {
        var token = tokenService.GetTokenFromHeader(); 
        await commentService.UpdateComment(commentId, editCommentDto, token);
        return Ok();
    }
    
    [Authorize(Policy = "TokenBlackListPolicy")]
    [HttpDelete("deletecomment")]
    [SwaggerOperation(Summary = "Удалить комментарий")]
    [SwaggerResponse(StatusCodes.Status200OK, "Комментарий удален")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Комментарий не найден")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        var token = tokenService.GetTokenFromHeader(); 
        await commentService.DeleteComment(commentId, token);
        return Ok();
    }
    
    [HttpGet("{commentId}/comments")]
    [SwaggerOperation(Summary = "Получить цепочку комментариев")]
    [SwaggerResponse(StatusCodes.Status200OK, "Цепочка комментариев получена", typeof(List<CommentDTO>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Комментарий не найден или удален")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка сервера", typeof(Error))]
    public async Task<IActionResult> GetCommentChain(Guid commentId)
    {
        try
        {
            var commentDtos = await commentService.GetCommentChain(commentId);
            return Ok(commentDtos);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error { Message = "Произошла внутренняя ошибка сервера." });
        }
    }
}