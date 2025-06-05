using AccountService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Write
{
    public class AccountDbContextWrite : DbContext
    {
        public AccountDbContextWrite(DbContextOptions<AccountDbContextWrite> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");
                entity.HasKey(e => e.AccountId);
                entity.Property(e => e.AccountId).ValueGeneratedOnAdd();
                entity.Property(e => e.AccountUsername).IsRequired().HasMaxLength(100);
                entity.Property(e => e.AccountPassword).IsRequired().HasMaxLength(200);
                entity.Property(e => e.AccountEmail).IsRequired().HasMaxLength(200);
                entity.Property(e => e.AccountFullName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.AccountDob).IsRequired(false);
                entity.Property(e => e.AccountGender).IsRequired();
                entity.Property(e => e.AccountPhone).HasMaxLength(15);
                entity.Property(e => e.AccountPhone).IsRequired(false);
                entity.Property(e => e.AccountImage).HasMaxLength(500);
                entity.Property(e => e.AccountImage).IsRequired(false);
                entity.Property(e => e.AccountRole).IsRequired().HasMaxLength(50);
                entity.Property(e => e.AccountStatus).IsRequired().HasMaxLength(50);
                entity.Property(e => e.AccountTicketRequest).HasDefaultValue(0);
            });
        }

        // DbSet properties for your entities can be added here
        // public DbSet<Account> Accounts { get; set; }
    }

}
