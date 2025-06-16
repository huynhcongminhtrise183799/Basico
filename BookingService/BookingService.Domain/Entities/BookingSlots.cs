using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Entities
{
	public class BookingSlots
	{
		public Guid BookingId { get; set; }

		public Guid SlotId { get; set; }

		public Booking Booking { get; set; }

		public Slot Slot { get; set; }
	}
}
