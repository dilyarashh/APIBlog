using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using socials.DBContext.DTO.User;
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

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        public string GetTokenFromHeader()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                string authorizationHeader = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                {
                    return authorizationHeader.Substring("Bearer ".Length);
                }
                return null;
            }
        }
        
        public string GetIdFromToken(string token)
        {
            var jwtToken = _tokenHandler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null) {
                Console.WriteLine(jwtToken);
            }

            var doctorId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            return doctorId;
        }
    }
