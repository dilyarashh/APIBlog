namespace socials.DBContext.DTO.Comment;

public class CommentDTO
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
    public string Author { get; set; } 
    public int SubComments { get; set; }
    public Guid? ParentId { get; set; }
    public Guid PostId { get; set; }
}
