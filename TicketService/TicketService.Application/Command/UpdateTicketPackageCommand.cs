using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.DTOs.Response;

namespace TicketService.Application.Command
{
    public record UpdateTicketPackageCommand(Guid id, string name, int amount, double price, string status) : IRequest<TicketPackageResponse>;

}
