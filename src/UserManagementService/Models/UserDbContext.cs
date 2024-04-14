using Microsoft.EntityFrameworkCore;

namespace UserManagementService.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }

        public DbSet<User> Users { get; set; }
    }
}