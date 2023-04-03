using Microsoft.EntityFrameworkCore;
using Payment_Gateway.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Payment_Gateway.Models.Entities;

namespace Payment_Gateway.DAL.Context
{
    public class PaymentGatewayDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, IdentityUserLogin<string>, ApplicationRoleClaim,
        IdentityUserToken<string>>
    {
        public PaymentGatewayDbContext(DbContextOptions<PaymentGatewayDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Payout> Payouts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<ApplicationUser>()
               .Property(u => u.LastName)
               .HasMaxLength(50)
               .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email, "IX_UniqueEmail")
                .IsUnique();
            modelBuilder.Entity<Wallet>()
                .Property(u => u.Balance).HasMaxLength(250);

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
