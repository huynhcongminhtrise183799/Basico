using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Entities
{
	public enum BookingStatus
	{
		Pending,
		CheckedIn,
		Cancelled,
		Completed,
		Paid
	}				
	public class Booking
	{
		public Guid BookingId { get; set; }

		public Guid? CustomerId { get; set; }

		public Guid LawyerId { get; set; }

		public Guid ServiceId { get; set; }

		public DateOnly BookingDate { get; set; }

		public double Price { get; set; }

		public string Status { get; set; }

		public virtual ICollection<BookingSlots> BookingSlots { get; set; } = new List<BookingSlots>();


	}
}
