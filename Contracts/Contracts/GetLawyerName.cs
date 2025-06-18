using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public class GetLawyerName
	{
		public Guid CorrelationId { get; set; }

		public Guid? CustomerId { get; set; }
		public Guid LawyerId { get; set; }

		public Guid ServiceId { get; set; }
		public string? LawyerName { get; set; }

		public string? CustomerName { get; set; }
		public string? ServiceName { get; set; }
	}
}
