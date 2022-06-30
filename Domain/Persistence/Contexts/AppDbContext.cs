using Algorithms.API.Domain.Models;
using Algorithms.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Algorithms.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Algorithm> Algorithms { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User Entity

            builder.Entity<User>().ToTable("Users");

            // Constraints

            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<User>().Property(p => p.Name).IsRequired();
            builder.Entity<User>().Property(p => p.LastName).IsRequired();
            builder.Entity<User>().Property(p => p.IsActive).IsRequired();
            builder.Entity<User>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // Relationships

            builder.Entity<UserLogin>()
               .HasOne(ul => ul.User)
               .WithOne(ul => ul.UserLogin)
               .HasForeignKey<User>(ul => ul.UserLoginId);
            builder.Entity<User>()
                .HasOne(ul => ul.UserLogin)
                .WithOne(ul => ul.User)
                .HasForeignKey<User>(ul => ul.UserLoginId);

            // Algorithm Entity

            builder.Entity<Algorithm>().ToTable("Algorithms");

            // Constraints

            builder.Entity<Algorithm>().HasKey(p => p.Id);
            builder.Entity<Algorithm>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<Algorithm>().Property(p => p.Name).IsRequired();
            builder.Entity<Algorithm>().Property(p => p.Description).IsRequired();
            builder.Entity<Algorithm>().Property(p => p.IsActive).IsRequired();
            builder.Entity<Algorithm>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Algorithm>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // Relationships

            builder.Entity<User>()
                .HasMany(uc => uc.Algorithms)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId);
            builder.Entity<Algorithm>()
                .HasOne(uc => uc.User)
                .WithMany(uc => uc.Algorithms)
                .HasForeignKey(uc => uc.UserId);

            // UserLogin Entity

            builder.Entity<UserLogin>().ToTable("UserLogins");

            // Constraints

            builder.Entity<UserLogin>().HasKey(p => p.Id);
            builder.Entity<UserLogin>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<UserLogin>().Property(p => p.Username).IsRequired();
            builder.Entity<UserLogin>().Property(p => p.Password).IsRequired();
            builder.Entity<UserLogin>().Property(p => p.IsActive).IsRequired();
            builder.Entity<UserLogin>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserLogin>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // Naming Conventions Policy

            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
