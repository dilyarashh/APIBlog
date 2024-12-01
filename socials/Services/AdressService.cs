using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using socials.DBContext;
using socials.DBContext.Models.GAR;
using socials.Services.IServices;
using socials.SupportiveServices.Adress;

namespace socials.Services;
public class AdressService(GARContext context) : IAdressService
{
    public Task<List<SearchAddressModel>> Search(long parentObjectId, string? query)
    {
        var hierarchyList = context.AsAdmHierarchies
            .Where(x => x.Parentobjid == parentObjectId)
            .AsQueryable();

        var addressList = new List<SearchAddressModel>();

        if (!hierarchyList.IsNullOrEmpty())
        {
            addressList = FindChildInAddrObj(hierarchyList);

            if (addressList.IsNullOrEmpty())
            {
                addressList = FindChildInHouses(hierarchyList);
            }
        }

        if (query != null)
        {
            addressList = addressList
                .Where(x => x.Text != null
                            && x.Text.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        return Task.FromResult(addressList);
    }
    public async Task<List<SearchAddressModel>> Chain(Guid objectGuid)
    {
        var house = context.AsHouses.FirstOrDefault(x =>
            x.Objectguid == objectGuid);

        var addressList = new List<SearchAddressModel>();

        if (house != null)
        {
            var searchHouseModel = new SearchAddressModel(
                house.Objectid,
                house.Objectguid,
                GetHouseName(house),
                Level.Building,
                AddressHelper.GetAddressName(10)
            );
            addressList.Add(searchHouseModel);

            var houseHierarchy = await context.AsAdmHierarchies
                .Where(x => x.Objectid == house.Objectid && x.Isactive == 1)
                .FirstOrDefaultAsync();

            if (houseHierarchy != null)
            {
            }

            var parentGuid = context.AsAddrObjs.FirstOrDefault(x =>
                houseHierarchy != null && x.Objectid == houseHierarchy.Parentobjid && x.Isactive == 1)!.Objectguid;

            objectGuid = parentGuid;
        }

        var address = context.AsAddrObjs.FirstOrDefault(x =>
            x.Objectguid == objectGuid && x.Isactive == 1);
        
        var searchAddressModel = new SearchAddressModel(
            address?.Objectid,
            address!.Objectguid,
            address.Typename + " " + address.Name,
            AddressHelper.GetGarAddressLevel(Convert.ToInt32(address.Level)),
            AddressHelper.GetAddressName(Convert.ToInt32(address.Level))
        );

        while (searchAddressModel != null)
        {
            addressList.Add(searchAddressModel);
            searchAddressModel = await FindParentInAddrObjOneObject(searchAddressModel.ObjectId);
        }

        addressList.Reverse();

        return addressList;
    }
    private List<SearchAddressModel> FindChildInAddrObj(IQueryable<Hierarchy> hierarchyList)
    {
        var addressQuery =
            from hierarchyItem in hierarchyList
            join address in context.AsAddrObjs
                on hierarchyItem.Objectid equals address.Objectid
            where address.Isactive == 1
            select new SearchAddressModel
            {
                ObjectId = address.Objectid,
                ObjectGuid = address.Objectguid,
                Text = $"{address.Typename} {address.Name}",
                ObjectLevel = AddressHelper.GetGarAddressLevel(Convert.ToInt32(address.Level)),
                ObjectLevelText = AddressHelper.GetAddressName(Convert.ToInt32(address.Level))
            };

        return addressQuery.ToList();
    }
    private List<SearchAddressModel> FindChildInHouses(IQueryable<Hierarchy> hierarchyList)
    {
        var addressQuery =
            from hierarchyItem in hierarchyList
            join address in context.AsHouses
                on hierarchyItem.Objectid equals address.Objectid
            where address.Isactive == 1
            select new SearchAddressModel
            {
                ObjectId = address.Objectid,
                ObjectGuid = address.Objectguid,
                Text = GetHouseName(address),
                ObjectLevel = Level.Building,
                ObjectLevelText = AddressHelper.GetAddressName(10)
            };
        
        return addressQuery.ToList();
    }
    private async Task<SearchAddressModel?> FindParentInAddrObjOneObject(long? objectId)
    {
        var result = await (from hierarchy in context.AsAdmHierarchies
                join address in context.AsAddrObjs on hierarchy.Parentobjid equals address.Objectid
                where hierarchy.Objectid == objectId && hierarchy.Isactive == 1 && address.Isactive == 1
                select new { address.Objectid, address.Objectguid, address.Typename, address.Name, address.Level }
            ).FirstOrDefaultAsync();

        if (result == null)
        {
            return null;
        }

        return new SearchAddressModel(
            result.Objectid,
            result.Objectguid,
            result.Typename + " " + result.Name,
            AddressHelper.GetGarAddressLevel(Convert.ToInt32(result.Level)),
            AddressHelper.GetAddressName(Convert.ToInt32(result.Level))
        );
    }
    private static string GetHouseName(House house)
    {
        return $"{house.Housenum} {(house.Addtype1.HasValue ? $"{AddressHelper.GetHouseType(house.Addtype1)} {house.Addnum1 ?? ""}" : "")} {(house.Addtype2.HasValue ? $"{AddressHelper.GetHouseType(house.Addtype2)} {house.Addnum2 ?? ""}" : "")}".Trim();
    }
}
