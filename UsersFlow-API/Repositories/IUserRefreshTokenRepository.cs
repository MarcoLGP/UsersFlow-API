using UsersFlow_API.Models;

namespace UsersFlow_API.Repositories
{
    public interface IUserRefreshTokenRepository
    {
        public Task addUserRefreshToken(string refreshToken, int userId);
        public Task removeUserRefreshToken(UserRefreshToken userRefreshToken);
        public Task<UserRefreshToken?> getUserRefreshToken(string refreshToken, int userId);
    }
}
