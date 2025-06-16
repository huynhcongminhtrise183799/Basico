using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Entities
{
	public class Slot
	{
		public Guid SlotId { get; set; }

		public TimeOnly SlotStartTime { get; set; }

		public TimeOnly SlotEndTime { get; set; }

		public virtual ICollection<BookingSlots> BookingSlots { get; set; } = new List<BookingSlots>();
	}
}
