using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Request
{
	public class ResetPasswordRequest
	{
		public string Email { get; set; }	
		public string NewPassword { get; set; }
	}
}
