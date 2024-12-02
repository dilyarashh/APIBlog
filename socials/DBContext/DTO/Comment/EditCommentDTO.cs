using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.DTO.Comment;

public class EditCommentDTO
{
    [Required]
    public string Content { get; set; }
}