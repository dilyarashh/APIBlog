using socials.DBContext.Models.Enums;

namespace socials.DBContext.DTO.Author;
public class AuthorDTO
{
    public string Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public int Posts { get; set; }
    public int Likes { get; set; }
    public DateTime Created { get; set; }
}