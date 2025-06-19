using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketService.Domain.Entities
{
	public enum TicketStatus
	{
		InProgress,
		ANSWERED
	}
	public class Ticket
	{
		public Guid TicketId { get; set; }

		public Guid UserId { get; set; }

		public Guid? StaffId { get; set; }

		public Guid ServiceId { get; set; }

		public string Content_Send { get; set; }

		public string? Content_Response { get; set; }
		
		public string Status { get; set; }

	}
}
