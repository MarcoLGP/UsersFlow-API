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
    }
}
