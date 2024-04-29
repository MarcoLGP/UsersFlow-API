using UsersFlow_API.Repositories;
using UsersFlow_API.Entities;

namespace UsersFlow_API.Services
{
    public class UserRecoveryPassTokenService : IUserRecoveryPassTokenService
    {
        private readonly IUserRecoveryPassTokenRepository _userRecoveryPassTokenRepository;

        public UserRecoveryPassTokenService(IUserRecoveryPassTokenRepository userRecoveryPassTokenRepository)
        {
            _userRecoveryPassTokenRepository = userRecoveryPassTokenRepository;
        }
        public async Task AddUserRecoveryPassToken(int userId, string recoveryPassToken)
        {
            await _userRecoveryPassTokenRepository.addUserRecoveryPassToken(new UserRecoveryPassToken 
            { 
                RecoveryPassToken = recoveryPassToken,
                UserId = userId
            });
        }

        public async Task<bool?> CheckUserRecoveryPassToken(string recoveryPassToken, int userId)
        {
            Console.WriteLine($"{recoveryPassToken}\n{userId}");
            var userRecoveryPassTokenFound = await _userRecoveryPassTokenRepository.getUserRecoveryPassToken(recoveryPassToken, userId);

            if (userRecoveryPassTokenFound is null)
                return null;

            return true;
        }

        public async Task<bool?> RemoveUserRecoveryPassToken(int userId, string recoveryPassToken)
        {
            var userRecoveryPassTokenFound = await _userRecoveryPassTokenRepository.getUserRecoveryPassToken(recoveryPassToken, userId);

            if (userRecoveryPassTokenFound is null)
                return null;

            await _userRecoveryPassTokenRepository.removeUserRecoveryPassToken(userRecoveryPassTokenFound);
            return true;
        }
    }
}
