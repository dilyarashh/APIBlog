using Microsoft.EntityFrameworkCore;
using socials.DBContext;
using socials.DBContext.DTO.Author;
using socials.Services.IServices;

namespace socials.Services;
public class AuthorService(AppDbcontext context) : IAuthorService
{
    public async Task<List<AuthorDTO>> GetAllAuthors()
    {
        return await (from author in context.Users
                join post in context.Posts on author.Id equals post.AuthorId into postsGroup
                where postsGroup.Any() 
                select new AuthorDTO
                {
                    Name = author.Name,
                    BirthDate = author.Birthday,
                    Gender = author.Gender,
                    Posts = postsGroup.Count(),
                    Likes = postsGroup.Sum(p => p.Likes),
                    Created = author.CreateTime
                }).GroupBy(x => x.Name)
            .Select(g => g.First())
            .ToListAsync();
    }
}