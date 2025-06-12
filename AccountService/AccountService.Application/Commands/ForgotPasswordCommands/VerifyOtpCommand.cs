using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Commands.ForgotPasswordCommands
{
	public class VerifyOtpCommand : IRequest<bool>
	{
		public string Email { get; }
		public string OTP { get; }

		public VerifyOtpCommand(string email,string otp)
		{
			Email = email;
			OTP = otp;
		}
	}

}
