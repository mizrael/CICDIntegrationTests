using Microsoft.EntityFrameworkCore;
using Users.Models;

namespace Users.Persistence
{
    public class UsersDbContext : DbContext, IUsersDbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityConfig = modelBuilder.Entity<User>();
            entityConfig.HasKey(u => u.Id);
            entityConfig.Property(u => u.FirstName).HasMaxLength(25).IsRequired();
            entityConfig.Property(u => u.LastName).HasMaxLength(25).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
