namespace socials.DBContext.Models.GAR;
public class Hierarchy
{
    public long Id { get; set; }
    public long Objectid { get; set; }
    public long? Parentobjid { get; set; }
    public int? Isactive { get; set; }
}