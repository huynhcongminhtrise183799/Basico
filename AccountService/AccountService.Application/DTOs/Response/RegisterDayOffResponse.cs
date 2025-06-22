using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Response
{
	public class RegisterDayOffResponse
	{
		public Guid DayOffId { get; set; }
		public Guid AccountId { get; set; }
		public DateOnly DayOff { get; set; }
		public List<SpecificLawyerDayOffSchedule> specificLawyerDayOffSchedules { get; set; }
	}
}
