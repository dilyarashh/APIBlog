using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using socials.DBContext;
using socials.DBContext.DTO.User;
using socials.DBContext.Models;
using socials.Services.IServices;
using socials.SupportiveServices.Exceptions;
using socials.SupportiveServices.Password;
using socials.SupportiveServices.Token;
using socials.SupportiveServices.Validations;

namespace socials.Services;

public class UserService : IUserService
{
    private readonly AppDBContext _dbContext;
    private readonly TokenInteractions _tokenService;
    private readonly HashPassword _hashPassword;

    public UserService(AppDBContext dbContext, TokenInteractions tokenService, HashPassword hashPassword)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
        _hashPassword = hashPassword;
    }

    public bool IsUniqueUser(string email)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
        if (user == null)
        {
            return true;
        }

        return false;
    }

    public async Task<TokenDTO> Register(RegistrationDTO registrationDto)
    {
        if (!IsUniqueUser(registrationDto.Email))
        {
            throw new BadRequestException("Email уже используется");
        }

        if (!NameValidator.IsValidName(registrationDto.Name))
        {
            throw new BadRequestException("Неправильный формат имени. Имя и фамилия должны начинаться с заглавной буквы. Допускаются только буквы и тире. Отчество является необязательным.");
        }

        if (!EmailValidator.ValidateEmail(registrationDto.Email))
        {
            throw new BadRequestException("Неверный формат email!");
        }

        if (!PhoneValidator.IsValidePhoneNumber(registrationDto.Phone))
        {
            throw new BadRequestException("Неверный формат номера телефона!");
        }

        if (!BirthdayValidator.ValidateBirthday(registrationDto.Birthday))
        {
            throw new BadRequestException("Дата рождения должна быть в пределах 01.01.1900 и не позднее нынешнего времени.");
        }

        if (!PasswordValidator.IsValidPassword(registrationDto.Password))
        {
            throw new BadRequestException("Пароль должен содержать минимум 8 символов, включая хотя бы одну заглавную букву, одну строчную букву, одну цифру и один специальный символ.");
        }

        string hashedPassword = _hashPassword.HashingPassword(registrationDto.Password);

        User user = new User()
        {
            Id = Guid.NewGuid(),
            Name = registrationDto.Name,
            Password = hashedPassword, 
            Email = registrationDto.Email,
            Birthday = registrationDto.Birthday,
            Gender = registrationDto.Gender,
            Phone = registrationDto.Phone,
            CreateTime = DateTime.UtcNow
        };
        
        await _dbContext.Users.AddAsync(user); 
        await _dbContext.SaveChangesAsync();

        var token = _tokenService.GenerateToken(user);

        return new TokenDTO
        {
            Token = token
        };
    }

    public async Task<TokenDTO> Login(LoginDTO loginDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(d => d.Email == loginDto.Email);
        if (user == null)
        {
            throw new BadRequestException("Неправильный Email или пароль");
        }
        else if (!HashPassword.VerifyPassword(loginDto.Password, user.Password))
        {
            throw new BadRequestException("Неправильный Email или пароль");
        }

        var token = _tokenService.GenerateToken(user);
        return new TokenDTO
        {
            Token = token
        };

    }
    
    public async Task<UserDTO> GetProfile(string token)
    {
        string userId = _tokenService.GetIdFromToken(token);
        var user = await _dbContext.Users.FirstOrDefaultAsync(d => d.Id == Guid.Parse(userId));
        if (user != null)
        {
            return new UserDTO
            {
                Id = user.Id,
                CreateTime = user.CreateTime,
                Name = user.Name,
                Birthday = user.Birthday,
                Gender = user.Gender,
                Email = user.Email,
                Phone = user.Phone
            };
        }
        else
        {
            throw new UnauthorizedException("Пользователь не авторизован");
        }
    }
    
    public async Task EditProfile(string token, EditDTO editDto)
        {
            string userId = _tokenService.GetIdFromToken(token);

            var user = await _dbContext.Users.FirstOrDefaultAsync(d => d.Id == Guid.Parse(userId));
            if (user != null)
            {
                if (editDto.Name != null) {
                    if (!NameValidator.IsValidName(editDto.Name))
                    {
                        throw new BadRequestException("Неправильный формат имени. Имя и фамилия должны начинаться с заглавной буквы. Допускаются только буквы и тире. Отчество является необязательным.");
                    }

                    user.Name = editDto.Name;
                }

                if (editDto.Phone != null) {
                    if (!PhoneValidator.IsValidePhoneNumber(editDto.Phone))
                    {
                        throw new BadRequestException("Неверный формат номера телефона!");
                    }
                    user.Phone = editDto.Phone;
                }

                if (editDto.Email != null)
                {
                    if (!EmailValidator.ValidateEmail(editDto.Email))
                    {
                        throw new BadRequestException("Неверный формат email!");
                    }
                    user.Email = editDto.Email;
                }

                if (editDto.Birthday != null)
                {
                    if (!BirthdayValidator.ValidateBirthday((DateTime)(editDto.Birthday)))
                    {
                        throw new BadRequestException("Дата рождения должна быть в пределах 01.01.1900 и не позднее нынешнего времени.");
                    }
                    user.Birthday = (DateTime)(editDto?.Birthday);
                }
                if (editDto.Gender != null) {
                    user.Gender = editDto.Gender;
                }

                await _dbContext.SaveChangesAsync();
            }

            else
            {
                throw new UnauthorizedException("Пользователь не авторизован");
            }
        }
    
    public async Task Logout(string token)
    {
        string id = _tokenService.GetIdFromToken(token);

        if (Guid.TryParse(id, out Guid doctorId) && doctorId != Guid.Empty)
        {
            await _dbContext.BlackTokens.AddAsync(new BlackToken { Blacktoken = token });
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new BadRequestException("Некорректный ID: не удалось извлечь или преобразовать id из токена.");
        }
    }
}
