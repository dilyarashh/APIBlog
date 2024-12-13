using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using socials.DBContext.Models;

namespace socials.SupportiveServices.Token;

public class TokenInteractions
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private JwtSecurityTokenHandler _tokenHandler;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenInteractions(IConfiguration configuration, IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
    {
        _secretKey = configuration.GetValue<string>("AppSettings:Secret");
        _issuer = configuration.GetValue<string>("AppSettings:Issuer");
        _audience = configuration.GetValue<string>("AppSettings:Audience");
        _serviceProvider = serviceProvider;
        _httpContextAccessor = httpContextAccessor;
        _tokenHandler = new JwtSecurityTokenHandler();
    }
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString())
        };

        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor); 
        return _tokenHandler.WriteToken(token); 
    }

    public string? GetTokenFromHeader() 
    {
        string? authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return null; 
        }
        return authorizationHeader.Substring("Bearer ".Length);
    }

    public string GetIdFromToken(string? token)
    {
        var jwtToken = _tokenHandler.ReadJwtToken(token); 
        if (jwtToken == null)
        {
            throw new SecurityTokenException("Invalid token"); 
        }

        var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
        if(string.IsNullOrEmpty(userId))
        {
            throw new SecurityTokenException("Invalid token: ClaimTypes.Sid not found");
        }

        return userId;
    }
}