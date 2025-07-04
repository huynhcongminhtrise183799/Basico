using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Event
{
	public class FeedbackUpdatedEvent
	{
		public Guid FeedbackId { get; set; }

		public string FeedbackContent { get; set; }

		public int Rating { get; set; }
	}
}
