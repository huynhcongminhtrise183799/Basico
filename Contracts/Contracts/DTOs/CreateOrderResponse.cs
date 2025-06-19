using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class CreateOrderResponse
    {
        public Guid OrderId { get; set; }
        public bool Success { get; set; }
    }
}
