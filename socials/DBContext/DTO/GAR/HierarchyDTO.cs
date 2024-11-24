namespace socials.DBContext.DTO.GAR;

public class HierarchyDTO
{
    public long Id { get; set; }
    
    public long? Objectid { get; set; }

    public long? Parentobjid { get; set; }
    
    public int? Isactive { get; set; }
}