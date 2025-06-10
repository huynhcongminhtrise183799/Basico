using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketService.Application.Command
{
    public record DeleteTicketPackageCommand(Guid id) : IRequest<bool>;

}
