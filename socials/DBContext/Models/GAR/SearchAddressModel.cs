namespace socials.DBContext.Models.GAR;
public class SearchAddressModel
{
    public long? ObjectId { get; set; }
    public Guid ObjectGuid { get; set; }
    public string? Text { get; set; }
    public Level? ObjectLevel { get; set; }
    public string? ObjectLevelText { get; set; }
    public SearchAddressModel() {}
    public SearchAddressModel(long? objectId, Guid objectGuid, String text, Level objectLevel,
        string objectLevelText)
    {
        ObjectId = objectId;
        ObjectGuid = objectGuid;
        Text = text;
        ObjectLevel = objectLevel;
        ObjectLevelText = objectLevelText;
    }
}