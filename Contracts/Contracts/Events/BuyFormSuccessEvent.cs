using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events
{
	public class BuyFormSuccessEvent
	{
		public Guid CustomerId { get; set; }

		public int Request { get; set; }
	}
}
