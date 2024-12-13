using socials.DBContext.Models;
using socials.DBContext.Models.GAR;

namespace socials.Services.IServices;

public interface IAdressService
{
    Task<List<SearchAdressModel>> Search(long parentObjectId, string? query);
    Task<List<SearchAdressModel>> Chain(Guid objectGuid);
}