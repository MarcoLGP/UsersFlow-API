using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace UsersFlow_API.Services
{
    public class TokenService : ITokenService
    {
        public JwtSecurityToken GenerateToken(IEnumerable<Claim> claims, IConfiguration configuration)
        {
            var key = configuration.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new InvalidOperationException("Invalid Secret Key !");
            var privateKey = Encoding.UTF8.GetBytes(key);
            
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
                Audience = configuration.GetSection("JWT").GetValue<string>("ValidAudience"),
                Issuer = configuration.GetSection("JWT").GetValue<string>("ValidIssuer"),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            
            return token;
        }
        public string GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[128];

            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(secureRandomBytes);
            var refreshToken = Convert.ToBase64String(secureRandomBytes);
            
            return refreshToken;
        }

        public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new InvalidOperationException("Invalid Secret Key !");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = configuration.GetSection("JWT").GetValue<string>("ValidAudience"),
                ValidIssuer = configuration.GetSection("JWT").GetValue<string>("ValidIssuer"),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.CurrentCulture))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
