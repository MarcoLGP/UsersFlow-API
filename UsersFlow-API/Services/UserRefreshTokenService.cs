using UsersFlow_API.Models;
using UsersFlow_API.Repositories;

namespace UsersFlow_API.Services
{
    public class UserRefreshTokenService : IUserRefreshTokenService
    {
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        public UserRefreshTokenService(IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<UserRefreshToken?> getUserRefreshToken(string refreshToken, int userId)
        {
            return await _userRefreshTokenRepository.getUserRefreshToken(refreshToken, userId);
        }
        public async Task updateUserRefreshToken(UserRefreshToken userRefreshToken, string newUserRefreshToken, int userId)
        {
            await _userRefreshTokenRepository.removeUserRefreshToken(userRefreshToken);
            await _userRefreshTokenRepository.addUserRefreshToken(newUserRefreshToken, userId);        
        }

        public async Task addUserRefreshToken(string userRefreshToken, int userId)
        {
            await _userRefreshTokenRepository.addUserRefreshToken(userRefreshToken, userId);
        }

        public async Task<bool?> removeUserRefreshToken(string userRefreshToken, int userId)
        {
            var userRefreshTokenFound = await getUserRefreshToken(userRefreshToken, userId);

            if (userRefreshTokenFound is null)
                return null;

            await _userRefreshTokenRepository.removeUserRefreshToken(userRefreshTokenFound);            
            return true;
        }
    }
}
