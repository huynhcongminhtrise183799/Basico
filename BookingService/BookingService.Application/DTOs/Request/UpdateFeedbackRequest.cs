using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.DTOs.Request
{
	public class UpdateFeedbackRequest
	{
		public string FeedbackContent { get; set; }

		public int Rating { get; set; }
	}
}
