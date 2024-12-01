using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.Models;

public class Comment
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime CreateTime { get; set; }
    
    [Required]
    public String Content { get; set; }
    
    public DateTime? ModifiedDate { get; set; }
    
    public DateTime? DeleteDate { get; set; }
    
    [Required]
    public Guid AuthorId { get; set; }
    
    [Required]
    public string Author { get; set; }
    
    [Required]
    public int SubComments { get; set; }
    
    public List<Comment> SubCommentsList { get; set; }
    
    public Guid? ParentCommentId { get; set; }
    
    public Guid PostId { get; set; }
    
    public virtual Post Post { get; set; }
}