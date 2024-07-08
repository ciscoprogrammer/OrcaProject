using Microsoft.EntityFrameworkCore;
using OrcaProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace OrcaProject.Data
{


    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        

        
        public DbSet<Request> Requests { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Request>().ToTable("Requests");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Account>().ToTable("Accounts");

            modelBuilder.Entity<Account>()
               .HasOne(a => a.User)    // Each Account has one User.
               .WithMany()             // A User can have many Accounts.
               .HasForeignKey(a => a.UserId)  // The foreign key in the Account table that points to the User.
               .IsRequired();

            modelBuilder.Entity<IdentityUserLogin<string>>()
       .HasKey(login => new { login.ProviderKey, login.LoginProvider });

            modelBuilder.Entity<IdentityUserToken<string>>()
        .HasKey(userToken => new { userToken.UserId, userToken.LoginProvider, userToken.Name });

            modelBuilder.Ignore<IdentityUserRole<string>>();
        }
    }

}
