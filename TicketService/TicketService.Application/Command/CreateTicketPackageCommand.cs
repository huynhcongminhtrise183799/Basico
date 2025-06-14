using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.DTOs.Response;

namespace TicketService.Application.Command
{
    public record CreateTicketPackageCommand(string name, int amount, double price) : IRequest<TicketPackageResponse>;

}
