using AccountService.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event.DayOff
{
	public class DayOffJustifiedEvent
	{
		public Guid lawyerIdScheduleId { get; set; }
		public List<JustifyDayOffRequest> requests { get; set; }
	}
}
