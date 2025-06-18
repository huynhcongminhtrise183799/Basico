using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Event
{
	public class CancelBookingEvent
	{
		public Guid BookingId { get; set; }
	}
}
