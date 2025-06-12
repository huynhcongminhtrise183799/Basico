using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Commands.ForgotPasswordCommands
{
	public class ForgotPasswordRequestCommand : IRequest<bool>
	{
		public string Email { get; }
		public ForgotPasswordRequestCommand(string email) => Email = email;
	}
}
