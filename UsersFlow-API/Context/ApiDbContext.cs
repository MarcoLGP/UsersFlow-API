using Microsoft.EntityFrameworkCore;
using UsersFlow_API.Models;

namespace UsersFlow_API.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
