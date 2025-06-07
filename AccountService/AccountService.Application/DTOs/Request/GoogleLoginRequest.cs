using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Request
{
	public class GoogleLoginRequest
	{
		public string IdToken { get; set; } = string.Empty;
	}
}
