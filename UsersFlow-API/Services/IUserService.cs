using UsersFlow_API.DTOs;

namespace UsersFlow_API.Services
{
    public interface IUserService
    {
        public Task<bool?> updateUserName(int userId, string newName);
        public Task<bool?> updateUserEmail(int userId, string newEmail);
        public Task<bool?> updateUserPassword(int userId, string newPassword, string oldPassword);
        public Task<bool?> removeUser(int userId);
    }
}
