using Microsoft.EntityFrameworkCore;
using socials.DBContext;
using socials.DBContext.DTO.Tag;
using socials.Services.IServices;

namespace socials.Services;

public class TagService : ITagService
{
    private readonly AppDBContext _context; 

    public TagService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<List<TagDTO>> GetTagsAsync()
    {
        return await _context.Tags 
            .Select(tag => new TagDTO
            {
                Name = tag.Name,
                Id = tag.Id,
                CreateTime = tag.CreateTime
            })
            .ToListAsync();
    }
}