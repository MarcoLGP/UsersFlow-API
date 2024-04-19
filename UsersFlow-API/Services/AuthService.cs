using UsersFlow_API.Models;
using UsersFlow_API.Repositories;

namespace UsersFlow_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;

        public AuthService(IUserRepository userRepository, ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }

        public async Task<User?> signInUser(string email, string password)
        {
            return await _userRepository.getUserByEmailAndPassword(email, _cryptoService.encrypt(password));
        }

        public async Task<User?> signUpUser(string name, string email, string password)
        {
            var userFound = await _userRepository.getUserByEmailAndPassword(email, _cryptoService.encrypt(password));
            if (userFound is not null)
            {
                return null;
            }

            return await _userRepository.addUser(new User { Email = email, Password = _cryptoService.encrypt(password), Name = name });
        }
    }
}
