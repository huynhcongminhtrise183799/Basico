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
    public class AutoCompletedBookingBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AutoCompletedBookingBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CompletedBookingStatusAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task CompletedBookingStatusAsync()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IBookingRepositoryWrite>();
                var repositoryRead = scope.ServiceProvider.GetRequiredService<IBookingRepositoryRead>();
                var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                var bookings = await repositoryRead.GetBookingOverTimeByBookingDate(DateOnly.FromDateTime(DateTime.Now));
                if (bookings == null || !bookings.Any())
                {
                    return;
                }
                foreach (var booking in bookings)
                {
                    booking.Status = BookingStatus.Completed.ToString();

                    await repository.UpdateBookingAsync(booking);

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
