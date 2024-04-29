using UsersFlow_API.DTOs;
using UsersFlow_API.Models;
using UsersFlow_API.Repositories;

namespace UsersFlow_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;

        public UserService(IUserRepository userRepository, ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }
        private async Task<bool?> UpdateUser(int userId, string propToUpdate, string newValue)
        {
            var userFound = await _userRepository.getUserById(userId);

            if (userFound is null)
                return null;

            if (propToUpdate == "email")
                userFound.Email = newValue;
            else if (propToUpdate == "name")
                userFound.Name = newValue;
            else
                return null;

            await _userRepository.updateUser(userFound);
            return true;
        }

        public async Task<bool?> removeUser(int userId)
        {
            var userFound = await _userRepository.getUserById(userId);
            
            if (userFound is null)
                return null;

            await _userRepository.deleteUser(userFound);
            return true;
        }

        public async Task<bool?> updateUserEmail(int userId, string newEmail)
        {
            var userFound = await _userRepository.getUserByEmail(newEmail);

            if (userFound is not null)
                return false;

            return await UpdateUser(userId, "email", newEmail);
        }

        public async Task<bool?> updateUserName(int userId, string newName)
        {
            return await UpdateUser(userId, "name", newName);
        }

        public async Task<bool?> updateUserPassword(int userId, string newPassword, string oldPassword)
        {
            var userFound = await _userRepository.getUserById(userId);

            if (userFound is null)
                return null;

            if (_cryptoService.decrypt(userFound.Password) != oldPassword)
                return false;

            userFound.Password = _cryptoService.encrypt(newPassword);
            await _userRepository.updateUser(userFound);
            
            return true;
        }

        public async Task<UserDTO?> getUserById(int userId)
        {
            var userFound =  await _userRepository.getUserById(userId);

            if (userFound is null)
                return null;

            return new UserDTO
            {
                Name = userFound.Name,
                Email = userFound.Email,
            };
        }

        public async Task<User?> getUserByEmail(string email)
        {
            return await _userRepository.getUserByEmail(email);
        }

        public async Task<bool?> updateUserPasswordRecovery(int userId, string newPassword)
        {
            var userFound = await _userRepository.getUserById(userId);

            if (userFound is null)
                return null;

            userFound.Password = _cryptoService.encrypt(newPassword);
            await _userRepository.updateUser(userFound);
            
            return true;
        }
    }
}
