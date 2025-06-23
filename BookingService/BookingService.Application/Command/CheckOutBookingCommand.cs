using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Command
{
	public record CheckOutBookingCommand(Guid bookingId) : IRequest<bool>;
	

}
