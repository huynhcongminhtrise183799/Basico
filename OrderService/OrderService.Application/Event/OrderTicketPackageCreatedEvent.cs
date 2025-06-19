using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Event
{
    public class OrderTicketPackageCreatedEvent
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public double TotalPrice { get; set; }

        public string Status { get; set; }

        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
    public class OrderDetailDTO
    {
        public Guid OrderDetailId { get; set; }
        public Guid? TicketPackageId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
