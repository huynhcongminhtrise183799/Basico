using AccountService.Application.DTOs.LawyerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Response
{
	public class LawyerWithServiceResponse
	{
		public Guid LawyerId { get; set; }

		public string FullName { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }

		public string Image { get; set; }

		public double PricePerHour { get; set; }
	}
}
