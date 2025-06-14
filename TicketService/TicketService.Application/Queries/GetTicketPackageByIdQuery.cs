using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.DTOs.Response;

namespace TicketService.Application.Queries
{
    public record GetTicketPackageByIdQuery(Guid id) : IRequest<TicketPackageResponse>;

}
