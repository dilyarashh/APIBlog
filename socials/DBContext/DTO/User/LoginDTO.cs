using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.DTO.User;

public class LoginDTO
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}