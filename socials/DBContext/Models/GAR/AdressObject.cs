using System.ComponentModel.DataAnnotations;

namespace socials.DBContext.Models.GAR;
public class AdressObject
{
    public long Id { get; set; }
    public long Objectid { get; set; }
    [Key]
    public Guid Objectguid { get; set; }
    public string Name { get; set; } = null!;
    public string? Typename { get; set; }
    public string Level { get; set; } = null!;
    public int? Isactive { get; set; }
    
}