using BookingService.Application.Command;
using BookingService.Application.DTOs.Response;
using BookingService.Application.Event;
using BookingService.Domain.Entities;
using BookingService.Domain.IRepository;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Handler.CommandHandler
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
    {
        private readonly IBookingRepositoryWrite _bookingRepo;
        private readonly IBookingSlotsRepositoryWrite _bookingSlotsRepo;
        private readonly IPublishEndpoint _publish;

        public CreateBookingHandler(IBookingRepositoryWrite bookingRepo, IBookingSlotsRepositoryWrite bookingSlotsRepo, IPublishEndpoint publishEndpoint)
        {
            _bookingRepo = bookingRepo;
            _bookingSlotsRepo = bookingSlotsRepo;
            _publish = publishEndpoint;
        }

        public async Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                BookingDate = request.bookingDate,
                Price = request.Price,
                CustomerId = request.CustomerId,
                LawyerId = request.lawyerId,
                ServiceId = request.ServiceId,
                Status = BookingStatus.Pending.ToString()
            };

            var bookingSlots = request.SlotId.Select(slotId => new BookingSlots
            {
                SlotId = Guid.Parse(slotId),
                BookingId = booking.BookingId
            }).ToList();

            await _bookingRepo.CreateBookingAsync(booking);
            await _bookingSlotsRepo.AddBookedSlotAsync(bookingSlots);

            var @event = new CreateBookingEvent
            {
                BookingId = booking.BookingId,
                BookingDate = booking.BookingDate,
                Price = booking.Price,
                CustomerId = booking.CustomerId,
                LawyerId = booking.LawyerId,
                ServiceId = booking.ServiceId,
                SlotId = request.SlotId,
                Status = booking.Status
            };
            await _publish.Publish(@event, cancellationToken);
            var response = new BookingResponse
            {
                BookingId = booking.BookingId,
                BookingDate = booking.BookingDate,
                Price = booking.Price,
                CustomerId = booking.CustomerId,
                LawyerId = booking.LawyerId,
                ServiceId = booking.ServiceId,
                SlotId = request.SlotId
            };
            return response;
        }
    }
}
