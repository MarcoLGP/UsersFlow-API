using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using UsersFlow_API.Services;

namespace UsersFlow_API.Utils
{
    public static class AppUtils
    {
        public static string RemovePrefixBearer(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return token;
            }

            const string bearerPrefix = "Bearer ";

            if (token.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return token.Substring(bearerPrefix.Length);
            }

            return token;
        }

        public static int GetIntTokenUserId(string tokenRequest, ITokenService _tokenService, IConfiguration _configuration)
        {
            var principal = _tokenService.GetClaimsPrincipalFromExpiredToken(tokenRequest, _configuration);
            var idUserToken = principal.FindFirstValue("Id");
            var intIdUser = int.Parse(idUserToken!);

            return intIdUser;
        }
    }
}
