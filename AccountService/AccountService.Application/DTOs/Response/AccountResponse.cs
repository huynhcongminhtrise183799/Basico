using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Response
{
	public class AccountResponse
	{
		public Guid AccountId { get; set; }
		public string Email { get; set; }
		public string? FullName { get; set; }
		public string? PhoneNumber { get; set; }

		public string? Image { get; set; }
		public string Status { get; set; }

	}
}
