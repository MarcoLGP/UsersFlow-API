using System.Security.Claims;
using UsersFlow_API.Models;

namespace UsersFlow_API.Services
{
    public interface IUserService
    {
        public Task<User?> signInUser(string email, string password);
        public Task<User?> signUpUser(string name, string email, string password);
    }
}
