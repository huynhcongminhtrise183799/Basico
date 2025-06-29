using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class OrderDetail
    {
        public Guid OrderDetailId { get; set; }

        public Guid OrderId { get; set; }

        public Guid? TicketPackageId { get; set; }

        public Guid? FormTemplateId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
		[JsonIgnore]
		public Order Order { get; set; }
    }
}
