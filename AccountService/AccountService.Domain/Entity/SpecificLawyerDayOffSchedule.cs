using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entity
{
	public enum ShiftStatus
	{
		APPROVE,
		REJECT
	}
	public class SpecificLawyerDayOffSchedule
	{
		public Guid LawyerDayOffScheduleId { get; set; }

		public Guid ShiftId { get; set; }

		public string Status { get; set; }

		public LawyerDayOffSchedule LawyerDayOffSchedule { get; set; }

		public Shift Shift { get; set; }
	}
}
