using BookingService.Application.DTOs.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Command
{
    public record UpdateBookingCommand(Guid bookingId, Guid lawyerId, Guid customerId, Guid serviceId, DateOnly bookingDate, List<string> slotId, double price, string Description) : IRequest<bool>;

}
