using System.ComponentModel.DataAnnotations;
using socials.DBContext.Models.Enums;

namespace socials.DBContext.DTO.User;

public class EditDTO
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public DateTime Birthday { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public string Phone { get; set; }
}