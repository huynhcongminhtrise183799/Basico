using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event.DayOff
{
	public class DayOffUpdatedEvent
	{
		public Guid LawyerScheduleDayOffId { get; set; }
		public DateOnly dayOff { get; set; }
		public List<string> shiftId { get; set; }
	}
}
