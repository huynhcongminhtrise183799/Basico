using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Response
{
	public class GoogleLoginResponse
	{
		public string Message { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public Guid AccountId { get; set; }
		public string FullName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
	}

}
