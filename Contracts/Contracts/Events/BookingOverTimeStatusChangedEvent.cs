using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events
{
	public class BookingOverTimeStatusChangedEvent
	{
		public Guid BookingId { get; set; }
		public string Status { get; set; }

	}
}
