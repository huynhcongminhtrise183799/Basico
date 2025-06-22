using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Response
{
	public class ShiftResponse
	{
		public Guid ShiftId { get; set; }

		public TimeOnly StartTime { get; set; }

		public TimeOnly EndTime { get; set; }
	}
}
