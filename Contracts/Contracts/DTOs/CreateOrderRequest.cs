using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public Guid TicketPackageId { get; set; }
        public int Quantity { get; set; }
    }
}
