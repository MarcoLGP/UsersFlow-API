using UsersFlow_API.Entities;

namespace UsersFlow_API.Repositories
{
    public interface IUserRecoveryPassTokenRepository
    {
        public Task addUserRecoveryPassToken(UserRecoveryPassToken userRecoveryPassToken);
        public Task removeUserRecoveryPassToken(UserRecoveryPassToken userRecoveryPassToken);
        public Task<UserRecoveryPassToken?> getUserRecoveryPassToken(string recoveryPassToken, int userId);
    }
}
