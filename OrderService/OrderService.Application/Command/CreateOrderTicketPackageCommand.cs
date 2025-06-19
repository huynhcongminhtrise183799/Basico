using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Command
{
    public class CreateOrderTicketPackageCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid TicketPackageId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
