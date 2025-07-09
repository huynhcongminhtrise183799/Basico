using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Event
{
	public class FeedbackCreatedEvent
	{
		public Guid FeedbackId { get; set; }

		public Guid BookingId { get; set; }

		public Guid CustomerId { get; set; }

		public DateOnly FeedbackDay { get; set; }

		public string FeedbackContent { get; set; }

		public int Rating { get; set; }
	}
}
