using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Write
{
	public class BookingDbContextWrite : DbContext
	{
		public BookingDbContextWrite(DbContextOptions<BookingDbContextWrite> options) : base(options)
		{
		}
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Slot> Slots { get; set; }
		public DbSet<BookingSlots> BookingSlots { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Booking>(entity =>
			{
				entity.ToTable("Booking");
				entity.HasKey(e => e.BookingId);
				entity.Property(e => e.BookingId).ValueGeneratedOnAdd();
			});
			modelBuilder.Entity<Slot>(entity =>
			{
				entity.ToTable("Slot");
				entity.HasKey(e => e.SlotId);
				entity.Property(e => e.SlotId).ValueGeneratedOnAdd();
			});
			modelBuilder.Entity<BookingSlots>(entity =>
			{
				entity.ToTable("BookingSlots");
				entity.HasKey(e => new { e.BookingId, e.SlotId });

				entity.HasOne(e => e.Booking)
					.WithMany(b => b.BookingSlots)
					.HasForeignKey(e => e.BookingId)
					.HasConstraintName("FK_BookingSlots_Booking");

				entity.HasOne(e => e.Slot)
					.WithMany(s => s.BookingSlots)
					.HasForeignKey(e => e.SlotId)
					.HasConstraintName("FK_BookingSlots_Slot");

			});

		}

		
	}

}
