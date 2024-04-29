using Microsoft.EntityFrameworkCore;
using UsersFlow_API.Context;
using UsersFlow_API.Entities;

namespace UsersFlow_API.Repositories
{
    public class UserRecoveryPassTokenRepository : IUserRecoveryPassTokenRepository
    {
        private readonly ApiDbContext _context;

        public UserRecoveryPassTokenRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task addUserRecoveryPassToken(UserRecoveryPassToken userRecoveryPassToken)
        {
            await _context.UserRecoveryPassTokens.AddAsync(userRecoveryPassToken);
            await _context.SaveChangesAsync();
        }

        public async Task<UserRecoveryPassToken?> getUserRecoveryPassToken(string recoveryPassToken, int userId)
        {
            return await _context.UserRecoveryPassTokens.FirstOrDefaultAsync(c => c.UserId == userId && c.RecoveryPassToken == recoveryPassToken);
        }

        public async Task removeUserRecoveryPassToken(UserRecoveryPassToken userRecoveryPassToken)
        {
            _context.UserRecoveryPassTokens.Remove(userRecoveryPassToken);
            await _context.SaveChangesAsync();
        }
    }
}
