using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entity
{
	public class LawyerDayOffSchedule
	{
		public Guid LawyerDayOffScheduleId { get; set; }

		public DateOnly OffDay { get; set; }

		public Guid LawyerId { get; set; }

		public virtual ICollection<SpecificLawyerDayOffSchedule> SpecificLawyerDayOffSchedules { get; set; } = new List<SpecificLawyerDayOffSchedule>();

		public Account Lawyer { get; set; }
	}
}
