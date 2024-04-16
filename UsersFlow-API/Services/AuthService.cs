using UsersFlow_API.Models;
using UsersFlow_API.Repositories;

namespace UsersFlow_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> signInUser(string email, string password)
        {
            return await _userRepository.getUserByEmailAndPassword(email, password);
        }

        public async Task<User?> signUpUser(string name, string email, string password)
        {
            var userFound = await _userRepository.getUserByEmailAndPassword(email, password);
            if (userFound is not null)
            {
                return null;
            }

            return await _userRepository.addUser(new User { Email = email, Password = password, Name = name });
        }
    }
}
