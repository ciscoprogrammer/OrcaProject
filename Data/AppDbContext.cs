using Microsoft.EntityFrameworkCore;
using OrcaProject.Models;
namespace OrcaProject.Data
{
    

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext() : base()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Request>().ToTable("Requests");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Account>().ToTable("Accounts");

            //modelBuilder.Entity<Account>()
            //   .HasOne(a => a.User)    // Each Account has one User.
            //   .WithMany()             // A User can have many Accounts.
            //   .HasForeignKey(a => a.UserId)  // The foreign key in the Account table that points to the User.
            //   .IsRequired();
        }
    }

}
