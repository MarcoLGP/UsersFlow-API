namespace UsersFlow_API.Services
{
    public interface IUserRecoveryPassTokenService
    {
        public Task AddUserRecoveryPassToken(int userId, string recoveryPassToken);
        public Task<bool?> RemoveUserRecoveryPassToken(int userId, string recoveryPassToken);
        public Task<bool?> CheckUserRecoveryPassToken(string recoveryPassToken, int userId);
    }
}
