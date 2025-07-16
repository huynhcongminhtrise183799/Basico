using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events
{
   public class OrderOverTimeStatusChangedEvent
    {
		public Guid OrderId { get; set; }
		public string NewStatus { get; set; }
	}
}
