using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Read
{
    public class OrderDbContextRead : DbContext
	{
		public OrderDbContextRead(DbContextOptions<OrderDbContextRead> options) : base(options)
		{
		}

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>(entity =>
			{
				entity.ToTable("Order");
				entity.HasKey(e => e.OrderId);
				entity.Property(e => e.OrderId).ValueGeneratedOnAdd();
			});

			modelBuilder.Entity<OrderDetail>(entity =>
			{
				entity.ToTable("OrderDetail");
				entity.HasKey(e => e.OrderDetailId);
				entity.Property(e => e.OrderDetailId).ValueGeneratedOnAdd();

				entity.HasOne(e => e.Order)
				.WithMany(o => o.OrderDetails)
					.HasForeignKey(e => e.OrderId)
					.HasConstraintName("FK_OrderDetail_Order");
			});
			modelBuilder.Entity<Payment>(entity =>
			{
				entity.ToTable("Payment");
				entity.HasKey(e => e.PaymentId);
				entity.Property(e => e.PaymentId).ValueGeneratedOnAdd();

				entity.HasOne(e => e.Order)
					.WithOne(o => o.Payment)
					.HasForeignKey<Payment>(e => e.OrderId)
					.HasConstraintName("FK_Payment_Order");
			});
		}
	}
}
