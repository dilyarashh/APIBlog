namespace socials.DBContext.DTO.GAR;

public class HouseDTO
{
    public long Id { get; set; }
    
    public long Objectid { get; set; }

    public Guid Objectguid { get; set; }

    public string? Housenum { get; set; }

    public string? Addnum1 { get; set; }

    public string? Addnum2 { get; set; }

    public int? Addtype1 { get; set; }

    public int? Addtype2 { get; set; }
    
    public int? Isactive { get; set; }
    
    public int Level { get; set; }
}