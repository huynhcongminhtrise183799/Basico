using BookingService.Application.Event;
using BookingService.Domain.IRepository;
using Contracts.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Consumer
{
    public class BookingOverTimeInDateStatusChangedConsumerService : IConsumer<BookingOverTimeInDateStatusChangedEvent>
    {
        private readonly IBookingRepositoryRead _repository;

        public BookingOverTimeInDateStatusChangedConsumerService(IBookingRepositoryRead repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<BookingOverTimeInDateStatusChangedEvent> context)
        {
            var message = context.Message;
            var booking = await _repository.GetBookingByIdAsync(message.BookingId);
        if (booking != null)
            {
                booking.Status = message.Status;
                await _repository.UpdateBookingAsync(booking);
            }
        }
    }
}
