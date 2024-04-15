using UsersFlow_API.Models;

namespace UsersFlow_API.Services
{
    public interface IUserRefreshTokenService
    {
        public Task<UserRefreshToken?> getUserRefreshToken(string refreshToken, int userId);
        public Task updateUserRefreshToken(UserRefreshToken userRefreshToken, string newUserRefreshToken, int userId);
        public Task addUserRefreshToken(string userRefreshToken, int userId);
    }
}
