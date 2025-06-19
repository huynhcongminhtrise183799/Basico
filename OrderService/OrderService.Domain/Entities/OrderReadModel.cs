using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class OrderReadModel
    {
        public Guid OrderId { get; set; }
        public Guid TicketPackageId { get; set; }
        public Guid UserId { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
