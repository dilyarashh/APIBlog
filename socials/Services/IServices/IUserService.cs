using socials.DBContext.DTO.User;
using socials.DBContext.Models;

namespace socials.Services.IServices;


public interface IUserService
{
    public Task<TokenDTO> Register(RegistrationDTO registrationDto);
    public Task<TokenDTO> Login(LoginDTO loginDto);
    public Task<UserDTO> GetProfile(string? token);
    public Task EditProfile(string? token, EditDTO userEditDTO);
    public Task Logout(string? token);
}