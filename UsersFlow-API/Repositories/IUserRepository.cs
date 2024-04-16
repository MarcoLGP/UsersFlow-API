using UsersFlow_API.Models;

namespace UsersFlow_API.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> getUserById(int userId);
        public Task<User?> addUser(User user);
        public Task<User?> updateUser(User user);
        public Task<bool?> deleteUser(User user);
        public Task<User?> getUserByEmailAndPassword(string email, string password);
    }
}
