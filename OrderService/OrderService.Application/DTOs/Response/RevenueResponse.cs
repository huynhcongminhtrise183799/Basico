using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DTOs.Response
{
	public class RevenueResponse
	{
		public string Period { get; set; }

		public double BookingRevenue { get; set; }

		public double OrderRevenue { get; set; }

		public double TotalRevenue { get; set; }
	}
}
