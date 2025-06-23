using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Request
{
	public class UpdateDayOffRequest
	{
		public DateOnly DayOff { get; set; }

		public List<string> ShiftId { get; set; } = new List<string>();
	}
}
