using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.DTOs.Request
{
	public class CheckFreeSlotsLawyer
	{
		public Guid LawyerId { get; set; }
		public DateOnly Date { get; set; }


	}
}
