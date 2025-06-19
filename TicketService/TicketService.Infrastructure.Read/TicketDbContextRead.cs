using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.Entities;

namespace TicketService.Infrastructure.Read
{
    public class TicketDbContextRead : DbContext
    {
        public TicketDbContextRead(DbContextOptions<TicketDbContextRead> options)
            : base(options)
        {
        }

        // Define DbSet properties for read models here, if any.
        // Example: public DbSet<YourReadModel> YourReadModels { get; set; }

        public DbSet<TicketPackage> TicketPackages { get; set; }

		public DbSet<Ticket> Tickets { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketPackage>(entity =>
            {
                entity.ToTable("TicketPackage");
                entity.HasKey(e => e.TicketPackageId);
                entity.Property(e => e.TicketPackageId).ValueGeneratedOnAdd();
                entity.Property(e => e.TicketPackageName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.RequestAmount).IsRequired();
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
            });
			modelBuilder.Entity<Ticket>(entity =>
			{
				entity.ToTable("Ticket");
				entity.HasKey(e => e.TicketId);
				entity.Property(e => e.TicketId).ValueGeneratedOnAdd();
				entity.Property(e => e.UserId).IsRequired();
				entity.Property(e => e.StaffId).IsRequired(false);
				entity.Property(e => e.ServiceId).IsRequired();
				entity.Property(e => e.Content_Send).IsRequired();
				entity.Property(e => e.Content_Response);
				entity.Property(e => e.Status).IsRequired();
			});

		}
    }

}
