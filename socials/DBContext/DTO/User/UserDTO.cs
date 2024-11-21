using System.ComponentModel.DataAnnotations;
using socials.DBContext.Models.Enums;

namespace socials.DBContext.DTO.User;

public class UserDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public DateTime Birthday { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Email { get; set; }
}