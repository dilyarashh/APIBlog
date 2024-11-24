namespace socials.DBContext.DTO.GAR;

public class AdressObjectDTO
{
    public long Id { get; set; }

    public long Objectid { get; set; }

    public Guid Objectguid { get; set; }
    
    public string Name { get; set; } = null!;

    public string? Typename { get; set; }

    public string Level { get; set; } = null!;
 
    public int? Isactive { get; set; }
}