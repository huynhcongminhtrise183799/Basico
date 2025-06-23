using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Event
{
	public class CustomerFormUpdatedEvent
	{
		public Guid CustomerFormId { get; set; }

		public string FormData { get; set; }
	}
}
