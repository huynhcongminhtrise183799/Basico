using AccountService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Read
{
    public class AccountDbContextRead : DbContext
    {
        public AccountDbContextRead(DbContextOptions<AccountDbContextRead> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<ForgotPassword> ForgotPasswords { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<LawyerSpecificService> LawyerSpecificServices { get; set; }
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
                entity.Property(e => e.AboutLawyer).IsRequired(false);
            });
            modelBuilder.Entity<ForgotPassword>(entity =>
            {
                entity.ToTable("ForgotPassword");
                entity.HasKey(e => e.ForgotPasswordId);
                entity.Property(e => e.ForgotPasswordId).ValueGeneratedOnAdd();
                entity.Property(e => e.OTP).IsRequired().HasMaxLength(6);
                entity.Property(e => e.ExpirationDate).IsRequired();
                entity.HasOne(e => e.Account)
                      .WithMany(a => a.ForgotPasswords)
                      .HasForeignKey(e => e.AccountId)
                      .HasConstraintName("FK_ForgotPassword_Account");
            });
            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");
                entity.HasKey(e => e.ServiceId);
                entity.Property(e => e.ServiceId).ValueGeneratedOnAdd();
                entity.Property(e => e.ServiceName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ServiceDescription).IsRequired();
            });

            modelBuilder.Entity<LawyerSpecificService>(entity =>
            {
                entity.ToTable("LawyerSpecificService");

                entity.HasKey(e => new { e.LawyerId, e.ServiceId });

                entity.HasOne(e => e.Account)
                      .WithMany(a => a.LawyerSpecificServices)
                      .HasForeignKey(e => e.LawyerId)
                      .HasConstraintName("FK_LawyerSpecificService_Account");

                entity.HasOne(e => e.Service)
                      .WithMany(s => s.LawyerSpecificServices)
                      .HasForeignKey(e => e.ServiceId)
                      .HasConstraintName("FK_LawyerSpecificService_Service");
            });
        }
    }
}
