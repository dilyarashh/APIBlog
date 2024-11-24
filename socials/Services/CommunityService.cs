using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using socials.DBContext;
using socials.DBContext.DTO.Community;
using socials.DBContext.DTO.User;
using socials.DBContext.Models;
using socials.DBContext.Models.Enums;
using socials.Services.IServices;
using socials.SupportiveServices.Exceptions;
using socials.SupportiveServices.Token;

namespace socials.Services;

public class CommunityService : ICommunityService
{
    private readonly AppDBContext _context;
    private readonly TokenInteractions _tokenService;

    public CommunityService(AppDBContext context, TokenInteractions tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<CommunityFullDTO?> GetCommunity(Guid id)
    {
        var community = await _context.Communities
            .Include(c => c.CommunityUsers) 
            .ThenInclude(cu => cu.User) 
            .FirstOrDefaultAsync(c => c.Id == id);


        if (community == null)
        {
            return null;
        }
        
        var communityFullDto = new CommunityFullDTO
        {
            Id = community.Id,
            CreateTime = community.CreateTime,
            Name = community.Name,
            Description = community.Description,
            IsClosed = community.IsClosed,
            SubscribersCount = community.SubscribersCount,
            Administrators = community.CommunityUsers
                .Where(cu => cu.Role == CommunityRole.Administrator)
                .Select(cu => new UserDTO
                {
                    Id = cu.UserId,
                    CreateTime = cu.User.CreateTime,
                    Name = cu.User.Name,
                    Birthday = cu.User.Birthday,
                    Gender = cu.User.Gender,
                    Email = cu.User.Email,
                    Phone = cu.User.Phone
                })
                .ToList()
        };

        return communityFullDto;
    }

    public async Task<List<CommunityUserDTO>> GetUserCommunity(string token)
    {
        string userIdString = _tokenService.GetIdFromToken(token);
        
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
        {
            return new List<CommunityUserDTO>(); 
        }

        return await _context.CommunityUsers
            .Where(cu => cu.UserId == userId)
            .Select(cu => new CommunityUserDTO
            {
                UserId = cu.UserId,
                CommunityId = cu.CommunityId,
                Role = cu.Role == 0 ? CommunityRole.Administrator : CommunityRole.Subscriber
            })
            .ToListAsync();
    }
    
    public async Task<List<CommunityDTO>> GetCommunityList()
    {
        return await _context.Communities 
            .Select(community => new CommunityDTO
            {
                CreateTime = community.CreateTime,
                Name = community.Name,
                Description = community.Description,
                IsClosed = community.IsClosed,
                SubscribersCount = community.SubscribersCount,
            })
            .ToListAsync();
    }
    
    public async Task<CommunityRole?> GetUserRoleAsync(Guid communityId, string token)
    {
        try
        {
            Guid userId = Guid.Parse(_tokenService.GetIdFromToken(token));

            if (userId == Guid.Empty)
            {
                return null; 
            }

            var communityUser = await _context.CommunityUsers
                .FirstOrDefaultAsync(cu => cu.UserId == userId && cu.CommunityId == communityId);

            if (communityUser == null || communityUser.Role == null) 
            {
                return null; 
            }
            else if (communityUser.Role == 0)
            {
                return CommunityRole.Administrator;
            }
            else if (communityUser.Role == (CommunityRole)1)
            {
                return CommunityRole.Subscriber;
            }
            else
            {
                return null;
            }
        }
        catch (FormatException)
        {
            return null;
        }
        catch (Exception ex)
        {
            return null; 
        }
    }
    
    public async Task Subscribe(Guid communityId, string token)
    {
        string userIdString = _tokenService.GetIdFromToken(token);
        if (string.IsNullOrEmpty(userIdString)) throw new ArgumentException("Некорректный токен");
        if (!Guid.TryParse(userIdString, out Guid userId)) throw new ArgumentException("Некорректный формат ID пользователя");
        
        var community = await _context.Communities.FindAsync(communityId);
        if (community == null) throw new NotFoundException("Сообщество не найдено");
        
        var existingSubscription = await _context.CommunityUsers.FirstOrDefaultAsync(s => s.UserId == userId && s.CommunityId == communityId);
        if (existingSubscription != null) throw new ArgumentException("Вы уже подписаны на это сообщество");
        
        _context.CommunityUsers.Add(new CommunityUser { UserId = userId, CommunityId = communityId, Role = (CommunityRole)1 });
        community.SubscribersCount++;

        await _context.SaveChangesAsync();
    }
    
    public async Task Unsubscribe(Guid communityId, string token)
    {
        string userIdString = _tokenService.GetIdFromToken(token);
        if (string.IsNullOrEmpty(userIdString)) throw new ArgumentException("Некорректный токен");
        if (!Guid.TryParse(userIdString, out Guid userId)) throw new ArgumentException("Некорректный формат ID пользователя");

        var community = await _context.Communities.FindAsync(communityId);
        if (community == null) throw new NotFoundException("Сообщество не найдено");

        var existingSubscription = await _context.CommunityUsers
            .FirstOrDefaultAsync(s => s.UserId == userId && s.CommunityId == communityId);

        if (existingSubscription == null)
        {
            throw new ArgumentException("Вы не подписаны на это сообщество");
        }

        _context.CommunityUsers.Remove(existingSubscription);
        community.SubscribersCount--;

        await _context.SaveChangesAsync();
    }
}