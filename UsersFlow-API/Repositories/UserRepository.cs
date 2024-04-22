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
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
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

        public async Task<User?> updateUser(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> getUserByEmailAndPassword(string email, string password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.Password == password);
        }

        public async Task<User?> getUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
