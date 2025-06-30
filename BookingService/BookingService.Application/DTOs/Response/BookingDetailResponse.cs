using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.DTOs.Response
{
	public class BookingDetailResponse
	{
		public Guid BookingId { get; set; }
		public DateOnly BookingDate { get; set; }
		public double Price { get; set; }

		public string Description { get; set; }
		public string LawyerName { get; set; }

		public string? CustomerName { get; set; }
		public string ServiceName { get; set; }

		public Guid? CustomerId { get; set; }
		public Guid LawyerId { get; set; }
		public Guid ServiceId { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }

		public string Status { get; set; }
	}
}
