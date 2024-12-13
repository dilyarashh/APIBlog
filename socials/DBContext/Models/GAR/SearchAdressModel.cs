namespace socials.DBContext.Models.GAR;
public class SearchAdressModel
{
    public long? ObjectId { get; set; }
    public Guid ObjectGuid { get; set; }
    public string? Text { get; set; }
    public Level? ObjectLevel { get; set; }
    public string? ObjectLevelText { get; set; }
    public SearchAdressModel() {}
    public SearchAdressModel(long? objectId, Guid objectGuid, string text, Level objectLevel,
        string objectLevelText)
    {
        ObjectId = objectId;
        ObjectGuid = objectGuid;
        Text = text;
        ObjectLevel = objectLevel;
        ObjectLevelText = objectLevelText;
    }
}