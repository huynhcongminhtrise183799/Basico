using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Request
{

	public class JustifyDayOffRequest
	{
		public Guid ShiftId { get; set; }

		public string Status { get; set; }

	}
}
