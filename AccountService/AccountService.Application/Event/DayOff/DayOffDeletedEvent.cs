using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event.DayOff
{
	public class DayOffDeletedEvent
	{
		public Guid LawyerDayOffScheduleId { get; set; }

	}
}
