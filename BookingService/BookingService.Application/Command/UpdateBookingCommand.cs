using BookingService.Application.DTOs.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Command
{
    public record UpdateBookingCommand(Guid BookingId, Guid LawyerId, Guid CustomerId, Guid ServiceId, DateOnly BookingDate, List<string> SlotId, double Price, string Description) : IRequest<bool>;

}
