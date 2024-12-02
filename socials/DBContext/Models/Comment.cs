using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace socials.DBContext.Models;

public class Comment
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime CreateTime { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    public DateTime? ModifiedDate { get; set; }
    
    public DateTime? DeleteDate { get; set; }
    
    [Required]
    public Guid AuthorId { get; set; }
    
    [ForeignKey("AuthorId")]
    public virtual User AuthorUser { get; set; }
    
    public string Author { get; set; }
    
    public int SubComments { get; set; }
    
    public virtual List<Comment> SubCommentsList { get; set; } = new List<Comment>();
    
    public Guid? ParentId { get; set; }
    
    [ForeignKey("ParentId")]
    public virtual Comment ParentComment { get; set; }

    [Required]
    public Guid PostId { get; set; }
    
    [ForeignKey("PostId")]
    public virtual Post Post { get; set; }
}