using UsersFlow_API.DTOs;
using UsersFlow_API.Models;

namespace UsersFlow_API.Services
{
    public interface IUserService
    {
        public Task<bool?> updateUserName(int userId, string newName);
        public Task<bool?> updateUserEmail(int userId, string newEmail);
        public Task<bool?> updateUserPassword(int userId, string newPassword, string oldPassword);
        public Task<bool?> updateUserPasswordRecovery(int userId, string newPassword);
        public Task<bool?> removeUser(int userId);
        public Task<UserDTO?> getUserById(int userId);
        public Task<User?> getUserByEmail(string email);
    }
}
