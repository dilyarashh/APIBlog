using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.DTO.Comment;

public class CreateCommentDTO
{
    [Required]
    public string Content { get; set; }
    public Guid? ParentId { get; set; }
}