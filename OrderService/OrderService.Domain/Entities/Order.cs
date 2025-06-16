using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }
    public class Order
    {
        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public double TotalPrice { get; set; }

        public string Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual Payment Payment { get; set; }
    }
}
