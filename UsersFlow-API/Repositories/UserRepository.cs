using Microsoft.EntityFrameworkCore;
using UsersFlow_API.Context;
using UsersFlow_API.Models;

namespace UsersFlow_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _dbContext;
        public UserRepository(ApiDbContext context)
        {
            _dbContext = context;
        }
        public async Task<User?> getUserById(int userId)
        {
            User? userFound = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            return userFound;
        }
        public async Task<bool?> deleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User?> addUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> updateUserPassword(User user, string newPassword)
        {
            user.Password = newPassword;
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> updateUserEmail(User user, string newEmail)
        {
            user.Email = newEmail;
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> getUserByEmailAndPassword(string email, string password)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user;
        }
    }
}
