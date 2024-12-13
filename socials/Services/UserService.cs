using Microsoft.EntityFrameworkCore;
using socials.DBContext;
using socials.DBContext.DTO.User;
using socials.DBContext.Models;
using socials.Services.IServices;
using socials.SupportiveServices.Exceptions;
using socials.SupportiveServices.Password;
using socials.SupportiveServices.Token;
using socials.SupportiveServices.Validations;

namespace socials.Services;
public class UserService(AppDbcontext dbcontext, TokenInteractions tokenService, HashPassword hashPassword)
    : IUserService
{
    private bool IsUniqueUserEmail(string email)
    {
        var user = dbcontext.Users.FirstOrDefault(x => x.Email == email);
        if (user == null)
        {
            return true;
        }
        return false;
    }
    private bool IsUniqueUserPhone(string phone)
    {
        var user = dbcontext.Users.FirstOrDefault(x => x.Phone == phone);
        if (user == null)
        {
            return true;
        }
        return false;
    }
    public async Task<TokenDTO> Register(RegistrationDTO registrationDto)
    {
        if (!IsUniqueUserEmail(registrationDto.Email))
        {
            throw new BadRequestException("Такой email уже используется");
        }
        
        if (!IsUniqueUserPhone(registrationDto.Phone))
        {
            throw new BadRequestException("Такой телефон уже используется");
        }

        if (!NameValidator.IsValidFullName(registrationDto.Name))
        {
            throw new BadRequestException("Неккоректное имя. Введите имя и фамилию с большой буквы, используйте только буквы. Длина имени и фамилии не менее двух букв");
        }

        if (!EmailValidator.ValidateEmail(registrationDto.Email))
        {
            throw new BadRequestException("Неверный формат email");
        }

        if (!PhoneValidator.IsValidePhoneNumber(registrationDto.Phone))
        {
            throw new BadRequestException("Неверный формат номера телефона");
        }

        if (!BirthdayValidator.ValidateBirthday(registrationDto.Birthday))
        {
            throw new BadRequestException("Дата рождения должна быть в пределах 01.01.1900 и не позднее нынешнего времени");
        }

        if (!PasswordValidator.IsValidPassword(registrationDto.Password))
        {
            throw new BadRequestException("Пароль должен содержать минимум 8 символов");
        }
        
        string hashedPassword = hashPassword.HashingPassword(registrationDto.Password);

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
        
        await dbcontext.Users.AddAsync(user); 
        await dbcontext.SaveChangesAsync();

        var token = tokenService.GenerateToken(user);
        return new TokenDTO
        {
            Token = token
        };
    }
    public async Task<TokenDTO> Login(LoginDTO loginDto)
    {
        if (!EmailValidator.ValidateEmail(loginDto.Email))
        {
            throw new BadRequestException("Неверный формат email. Для авторизации введите ваш email.");
        }
        
        var user = await dbcontext.Users.FirstOrDefaultAsync(d => d.Email == loginDto.Email);
        if (user == null)
        {
            throw new BadRequestException("Неправильный email");
        }
        else if (!HashPassword.VerifyPassword(loginDto.Password, user.Password))
        {
            throw new BadRequestException("Неправильный пароль");
        }

        var token = tokenService.GenerateToken(user);
        return new TokenDTO
        {
            Token = token
        };
    }
    public async Task<UserDTO> GetProfile(string? token)
    {
        var userId = tokenService.GetIdFromToken(token);
        var user = await dbcontext.Users.FirstOrDefaultAsync(d => d.Id == Guid.Parse(userId));
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
    
    public async Task EditProfile(string? token, EditDTO editDto)
        {
            var userId = tokenService.GetIdFromToken(token);
            var user = await dbcontext.Users.FirstOrDefaultAsync(d => d.Id == Guid.Parse(userId));
            
            if (user != null)
            {
                if (!NameValidator.IsValidFullName(editDto.Name))
                {
                    throw new BadRequestException("Неккоректное имя. Введите имя и фамилию с большой буквы, используйте только буквы. Длина имени и фамилии не менее двух букв");
                }

                user.Name = editDto.Name;

                if (!PhoneValidator.IsValidePhoneNumber(editDto.Phone))
                {
                    throw new BadRequestException("Неверный формат номера телефона");
                }

                if (editDto.Phone != user.Phone)
                {
                    if (!IsUniqueUserPhone(editDto.Phone))
                    {
                        throw new BadRequestException("Такой телефон уже используется");
                    }
                    user.Phone = editDto.Phone;
                }
                user.Phone = editDto.Phone;

                if (!EmailValidator.ValidateEmail(editDto.Email))
                {
                    throw new BadRequestException("Неверный формат email");
                }

                if (editDto.Email != user.Email)
                {
                    if (!IsUniqueUserEmail(editDto.Email))
                    {
                        throw new BadRequestException("Такой email уже используется");
                    }
                    user.Email = editDto.Email;
                }
                user.Email = editDto.Email;

                if (!BirthdayValidator.ValidateBirthday((DateTime)(editDto.Birthday)))
                {
                    throw new BadRequestException("Дата рождения должна быть в пределах 01.01.1900 и не позднее нынешнего времени");
                }
                user.Birthday = (DateTime)(editDto?.Birthday!);

                user.Gender = editDto.Gender;

                await dbcontext.SaveChangesAsync();
            }

            else
            {
                throw new UnauthorizedException("Пользователь не авторизован");
            }
        }
    public async Task Logout(string? token)
    {
        string id = tokenService.GetIdFromToken(token);

        if (Guid.TryParse(id, out Guid userId) && userId != Guid.Empty)
        {
            await dbcontext.BlackTokens.AddAsync(new BlackToken { Blacktoken = token, CreatedAt = DateTime.UtcNow });
            await dbcontext.SaveChangesAsync();
        }
        else
        {
            throw new UnauthorizedException("Не удалось осуществить выход");
        }
    }
}
