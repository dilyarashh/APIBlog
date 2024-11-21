using socials.DBContext.DTO.Tag;

namespace socials.Services.IServices;

public interface ITagService
{
    Task<List<TagDTO>> GetTagsAsync();
}