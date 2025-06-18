using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entity
{
	public class Shift
	{
		public Guid ShiftId { get; set; }

		public TimeOnly StartTime { get; set; }

		public TimeOnly EndTime { get; set; }

		public virtual ICollection<SpecificLawyerDayOffSchedule> SpecificLawyerDayOffSchedules { get; set; } = new List<SpecificLawyerDayOffSchedule>();
	}
}
