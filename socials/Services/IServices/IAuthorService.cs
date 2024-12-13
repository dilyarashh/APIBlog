using socials.DBContext.DTO.Author;

namespace socials.Services.IServices;
public interface IAuthorService
{
    Task<List<AuthorDTO>> GetAllAuthors();
}