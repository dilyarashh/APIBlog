using Microsoft.EntityFrameworkCore;
using socials.DBContext;
using socials.DBContext.DTO.Tag;
using socials.Services.IServices;

namespace socials.Services;

public class TagService(AppDBContext context) : ITagService
{
    public async Task<List<TagDTO>> GetTags()
    {
        return await context.Tags 
            .Select(tag => new TagDTO
            {
                Name = tag.Name,
                Id = tag.Id,
                CreateTime = tag.CreateTime
            })
            .ToListAsync();
    }
}