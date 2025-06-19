using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.DTOs.Response
{
	public class BookingResponse
	{
		public Guid BookingId { get; set; }
		public DateOnly BookingDate { get; set; }

		public double Price { get; set; }

		public Guid? CustomerId { get; set; }

		public Guid LawyerId { get; set; }

		public Guid ServiceId { get; set; }

		public List<string> SlotId { get; set; } = new List<string>();
	}
}
