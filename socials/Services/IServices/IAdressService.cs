using socials.DBContext.Models;
using socials.DBContext.Models.GAR;

namespace socials.Services.IServices;

public interface IAdressService
{
    Task<List<SearchAddressModel>> Search(long parentObjectId, string? query);
    Task<List<SearchAddressModel>> Chain(Guid objectGuid);
}