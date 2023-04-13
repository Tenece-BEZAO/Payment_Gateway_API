 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Payment_Gateway.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Payment_Gateway.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.DAL.Context
{
    public class PaymentGatewayDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, IdentityUserLogin<string>, ApplicationRoleClaim,
        IdentityUserToken<string>>
    {
        public PaymentGatewayDbContext(DbContextOptions<PaymentGatewayDbContext> options)
            : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<AdminProfile> AdminProfiles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<Payout> Payouts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<User>()
               .Property(u => u.LastName)
               .HasMaxLength(50)
               .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email, "IX_UniqueEmail")
                .IsUnique();
            modelBuilder.Entity<Wallet>()
               .HasKey(a => a.WalletId);

            modelBuilder.Entity<Wallet>(p =>
            {
                p.Property(p => p.WalletId)
                    .ValueGeneratedOnAdd();

                p.Property(p => p.Balance)
                    .HasDefaultValue(0);
            });

            modelBuilder.Entity<ApiKey>(p =>
            {
                p.Property(p => p.ApiSecretKey)
                    .ValueGeneratedOnAdd();

                p.HasKey(p=> p.ApiSecretKey);
            });

            /*modelBuilder.Entity<ApplicationUser>()
                .HasOne(t1 => t1.Wallet)
                .WithOne(t2 => t2.)
                .HasForeignKey<Wallet>(t2 => t2.Id)
                .OnDelete(DeleteBehavior.Cascade);*/


            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
