using BookingService.Domain.Entities;
using BookingService.Domain.IRepository;
using Contracts.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Write.BackgroundServices
{
	public class BookingStatusBackgroundService : BackgroundService
	{
		private readonly IServiceScopeFactory _scopeFactory;

		public BookingStatusBackgroundService(IServiceScopeFactory scopeFactory)
		{
			_scopeFactory = scopeFactory;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				await UpdateBookingStatusAsync();
				await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
			}
		}

		private async Task UpdateBookingStatusAsync()
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var repository = scope.ServiceProvider.GetRequiredService<IBookingRepositoryWrite>();
				var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

				var bookings = await repository.GetBookingOverTimeAsync();

				foreach (var booking in bookings)
				{
					booking.Status = BookingStatus.Cancelled.ToString();
					await repository.UpdateBookingAsync(booking);
					Console.WriteLine($"Booking {booking.BookingId} status updated to {booking.Status} due to timeout.");
					await publishEndpoint.Publish(new BookingOverTimeStatusChangedEvent
					{
						BookingId = booking.BookingId,
						Status = booking.Status,
					});
				}
			}
		}
	}
}
