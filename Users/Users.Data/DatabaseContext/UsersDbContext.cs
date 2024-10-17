using Microsoft.EntityFrameworkCore;
using Users.Core.Models;

namespace Users.Data.DatabaseContext
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}