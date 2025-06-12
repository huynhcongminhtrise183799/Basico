using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.Entities;

namespace TicketService.Infrastructure.Write
{
    public class TicketDbContextWrite : DbContext
    {
        public TicketDbContextWrite(DbContextOptions<TicketDbContextWrite> options)
            : base(options)
        {
        }


         public DbSet<TicketPackage> TicketPackages { get; set; }

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
        }
    }
}
