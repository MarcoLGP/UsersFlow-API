using Microsoft.EntityFrameworkCore;
using UsersFlow_API.Context;
using UsersFlow_API.Models;

namespace UsersFlow_API.Repositories
{
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly ApiDbContext _context;
        public UserRefreshTokenRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task addUserRefreshToken(string refreshToken, int userId)
        {
            await _context.UserRefreshTokens.AddAsync(new UserRefreshToken { RefreshToken = refreshToken, UserId = userId });
            await _context.SaveChangesAsync();
        }

        public async Task<UserRefreshToken?> getUserRefreshToken(string refreshToken, int userId)
        {
            return await _context.UserRefreshTokens.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.UserId == userId);
        }

        public async Task removeUserRefreshToken(UserRefreshToken userRefreshToken)
        {
            _context.UserRefreshTokens.Remove(userRefreshToken);
            await _context.SaveChangesAsync();
        }
    }
}
