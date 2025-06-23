using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Command
{
	public record UpdateCustomerFormCommand(Guid CustomerFormId, string FormData) : IRequest<bool>;

}
