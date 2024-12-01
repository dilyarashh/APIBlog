using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using socials.DBContext;

namespace socials.SupportiveServices.Token;
public class TokenBlackListPolicy : AuthorizationHandler<TokenBlackListRequirment>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration; // Add IConfiguration

    public TokenBlackListPolicy(IServiceProvider serviceProvider, IConfiguration configuration) // Add IConfiguration
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration; // Initialize IConfiguration
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenBlackListRequirment requirement)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbcontext>();
            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            string authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length);

                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    var secretKey = Encoding.UTF8.GetBytes(_configuration["AppSettings:Secret"]); // Get secret key from configuration
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = _configuration["AppSettings:Issuer"],
                        ValidAudience = _configuration["AppSettings:Audience"],
                        ClockSkew = TimeSpan.Zero // Prevent time-based attacks
                    };

                    tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                    // Token is valid; check blacklist
                    var blackToken = await appDbContext.BlackTokens.FirstOrDefaultAsync(b => b.Blacktoken == token); // Use async method
                    if (blackToken != null)
                    {
                        context.Fail();
                    }
                    else
                    {
                        context.Succeed(requirement);
                    }
                }
                catch (SecurityTokenExpiredException)
                {
                    context.Fail(); // Handle expired tokens
                }
                catch (SecurityTokenException)
                {
                    context.Fail(); // Handle invalid tokens
                }
                catch (Exception ex)
                {
                    context.Fail(); // Handle other exceptions appropriately
                    // Log the exception
                }
            }
            else
            {
                context.Fail();
            }
        }
    }
}