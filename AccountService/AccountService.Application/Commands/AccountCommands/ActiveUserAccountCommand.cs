using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Commands.AccountCommands
{
	public record ActiveUserAccountCommand(Guid accountId) : IRequest<bool>;
	

}
