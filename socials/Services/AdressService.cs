using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using socials.DBContext;
using socials.DBContext.Models.GAR;
using socials.Services.IServices;
using socials.SupportiveServices.Adress;

namespace socials.Services;
public class AdressService(GARContext context) : IAdressService
{
    public Task<List<SearchAdressModel>> Search(long parentObjectId, string? query)
    {
        var hierarchyList = context.AsAdmHierarchies
            .Where(x => x.Parentobjid == parentObjectId)
            .AsQueryable();

        var adressList = new List<SearchAdressModel>();

        if (!hierarchyList.IsNullOrEmpty())
        {
            adressList = FindChildInAddrObj(hierarchyList);

            if (adressList.IsNullOrEmpty())
            {
                adressList = FindChildInHouses(hierarchyList);
            }
        }

        if (query != null)
        {
            adressList = adressList
                .Where(x => x.Text != null
                            && x.Text.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        return Task.FromResult(adressList);
    }
    public async Task<List<SearchAdressModel>> Chain(Guid objectGuid)
    {
        var house = context.AsHouses.FirstOrDefault(x =>
            x.Objectguid == objectGuid);

        var adressList = new List<SearchAdressModel>();

        if (house != null)
        {
            var searchHouseModel = new SearchAdressModel(
                house.Objectid,
                house.Objectguid,
                GetHouseName(house),
                Level.Building,
                AdressHelper.GetAddressName(10)
            );
            adressList.Add(searchHouseModel);

            var houseHierarchy = await context.AsAdmHierarchies
                .Where(x => x.Objectid == house.Objectid)
                .FirstOrDefaultAsync();

            if (houseHierarchy != null)
            {
            }

            var parentGuid = context.AsAddrObjs.FirstOrDefault(x =>
                houseHierarchy != null && x.Objectid == houseHierarchy.Parentobjid)!.Objectguid;

            objectGuid = parentGuid;
        }

        var adress = context.AsAddrObjs.FirstOrDefault(x =>
            x.Objectguid == objectGuid);
        
        var searchAddressModel = new SearchAdressModel(
            adress?.Objectid,
            adress!.Objectguid,
            adress.Typename + " " + adress.Name,
            AdressHelper.GetGarAddressLevel(Convert.ToInt32(adress.Level)),
            AdressHelper.GetAddressName(Convert.ToInt32(adress.Level))
        );

        while (searchAddressModel != null)
        {
            adressList.Add(searchAddressModel);
            searchAddressModel = await FindParentInAddrObjOneObject(searchAddressModel.ObjectId);
        }

        adressList.Reverse();

        return adressList;
    }
    private List<SearchAdressModel> FindChildInAddrObj(IQueryable<Hierarchy> hierarchyList)
    {
        var adressQuery =
            from hierarchyItem in hierarchyList
            join adress in context.AsAddrObjs
                on hierarchyItem.Objectid equals adress.Objectid
            select new SearchAdressModel
            {
                ObjectId = adress.Objectid,
                ObjectGuid = adress.Objectguid,
                Text = $"{adress.Typename} {adress.Name}",
                ObjectLevel = AdressHelper.GetGarAddressLevel(Convert.ToInt32(adress.Level)),
                ObjectLevelText = AdressHelper.GetAddressName(Convert.ToInt32(adress.Level))
            };

        return adressQuery.ToList();
    }
    private List<SearchAdressModel> FindChildInHouses(IQueryable<Hierarchy> hierarchyList)
    {
        var addressQuery =
            from hierarchyItem in hierarchyList
            join address in context.AsHouses
                on hierarchyItem.Objectid equals address.Objectid
            select new SearchAdressModel
            {
                ObjectId = address.Objectid,
                ObjectGuid = address.Objectguid,
                Text = GetHouseName(address),
                ObjectLevel = Level.Building,
                ObjectLevelText = AdressHelper.GetAddressName(10)
            };
    
        return addressQuery.ToList();
    }

    private async Task<SearchAdressModel?> FindParentInAddrObjOneObject(long? objectId)
    {
        var result = await (from hierarchy in context.AsAdmHierarchies
                join address in context.AsAddrObjs on hierarchy.Parentobjid equals address.Objectid
                where hierarchy.Objectid == objectId
                select new { address.Objectid, address.Objectguid, address.Typename, address.Name, address.Level }
            ).FirstOrDefaultAsync();

        if (result == null)
        {
            return null;
        }

        return new SearchAdressModel(
            result.Objectid,
            result.Objectguid,
            result.Typename + " " + result.Name,
            AdressHelper.GetGarAddressLevel(Convert.ToInt32(result.Level)),
            AdressHelper.GetAddressName(Convert.ToInt32(result.Level))
        );
    }

    private static string GetHouseName(House house)
    {
        return $"{house.Housenum} {(house.Addtype1.HasValue ? $"{AdressHelper.GetHouseType(house.Addtype1)} {house.Addnum1 ?? ""}" : "")} {(house.Addtype2.HasValue ? $"{AdressHelper.GetHouseType(house.Addtype2)} {house.Addnum2 ?? ""}" : "")}".Trim();
    }
}
