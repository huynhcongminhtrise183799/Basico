using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Event
{
	public class UpdateOrderStatusEvent
	{
		public Guid? OrderId { get; set; }
	}
}
