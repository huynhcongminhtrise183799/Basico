using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Event
{
	public class CheckInBookingEvent
	{
		public Guid BookingId { get; set; }
	}
}
