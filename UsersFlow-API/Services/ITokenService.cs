using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UsersFlow_API.Services
{
    public interface ITokenService
    {
        public JwtSecurityToken GenerateToken(IEnumerable<Claim> claims, IConfiguration configuration);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token, IConfiguration configuration);
        public bool IsValidToken(string token, IConfiguration configuration);
    }
}
