using socials.DBContext.Models.GAR;

namespace socials.SupportiveServices.Adress;
public static class AddressHelper
{
    private static readonly Dictionary<int, Level> LevelMappings = new Dictionary<int, Level>
    {
        { 1, Level.Region },
        { 2, Level.AdministrativeArea },
        { 3, Level.MunicipalArea },
        { 4, Level.RuralUrbanSettlement },
        { 5, Level.City },
        { 6, Level.Locality },
        { 7, Level.ElementOfPlanningStructure },
        { 8, Level.ElementOfRoadNetwork },
        { 9, Level.Land },
        { 10, Level.Building },
        { 11, Level.Room },
        { 12, Level.RoomInRooms },
        { 13, Level.AutonomousRegionLevel },
        { 14, Level.IntracityLevel },
        { 15, Level.AdditionalTerritoriesLevel },
        { 16, Level.LevelOfObjectsInAdditionalTerritories },
        { 17, Level.CarPlace },
    };

    private static readonly Dictionary<int, string> AddressNameMappings = new Dictionary<int, string>
    {
        { 1, "Субъект РФ" },
        { 2, "Административный район" },
        { 3, "Муниципальный район" },
        { 4, "Сельское/городское поселение" },
        { 5, "Город" },
        { 6, "Населенный пункт" },
        { 7, "Элемент планировочной структуры" },
        { 8, "Элемент улично-дорожной сети" },
        { 9, "Земельный участок" },
        { 10, "Здание (сооружение)" },
        { 11, "Помещение" },
        { 12, "Помещения в пределах помещения" },
        { 13, "Уровень автономного округа" },
        { 14, "Уровень внутригородской территории" },
        { 15, "Уровень дополнительных территорий" },
        { 16, "Уровень объектов на дополнительных территориях" },
        { 17, "Машиноместо" },
    };

    private static readonly Dictionary<int, string> HouseTypeMappings = new Dictionary<int, string>
    {
        { 1, "корпус" },
        { 2, "строение" },
        { 3, "сооружение" },
        { 4, "литера" },
    };
    public static Level GetGarAddressLevel(int? level) => LevelMappings.GetValueOrDefault(level ?? 1);
    public static string GetAddressName(int? level) => AddressNameMappings.GetValueOrDefault(level ?? 0) ?? string.Empty;
    public static string GetHouseType(int? type) => HouseTypeMappings.GetValueOrDefault(type ?? 0) ?? string.Empty;
}