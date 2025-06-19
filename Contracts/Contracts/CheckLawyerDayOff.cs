using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public class CheckLawyerDayOff
	{
		public Guid CorrelationId { get; set; }
		public Guid LawyerId { get; set; }
		public DateOnly DayOffDate { get; set; }

		public List<ShiftOff>? ShiftOffs { get; set; }

	}

	public class ShiftOff
	{
		public TimeOnly StartTime { get; set; }

		public TimeOnly EndTime { get; set; }
	}

}
