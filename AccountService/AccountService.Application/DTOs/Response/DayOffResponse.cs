using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Response
{
	public class DayOffResponse
	{
		public Guid DayOffId { get; set; }

		public Guid LawyerId { get; set; } // The ID of the account associated with the day off

		public string LawyerName { get; set; } // Name of the lawyer taking the day off

		public DateOnly DayOff { get; set; } // The date of the day off

		public List<SpecificDayOffResponse> SpecificDayOffs { get; set; } // List of specific day off details

	}

	public class SpecificDayOffResponse
	{
		public Guid ShiftId { get; set; } // The ID of the specific shift for the day off
		public TimeOnly FromTime { get; set; } // Start time of the day off

		public TimeOnly ToTime { get; set; } // End time of the day off

		public string Status { get; set; } // Status of the day off (e.g., "Pending", "Approved", "Rejected")
	}
}
