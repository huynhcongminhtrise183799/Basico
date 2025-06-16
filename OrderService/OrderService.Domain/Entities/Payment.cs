using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed
    }
    public enum PaymentMethod
    {
        VNPAY
    }
    public class Payment
    {
        public Guid PaymentId { get; set; }

        public Guid? OrderId { get; set; }

        public Guid? BookingId { get; set; } // Optional, if the payment is related to a booking

        public double Amount { get; set; }

        public string PaymentMethod { get; set; } // e.g., CreditCard, PayPal, etc.

        public DateTime PaymentDate { get; set; }

        public string Status { get; set; } // e.g., Pending, Completed, Failed

        public Order Order { get; set; }
    }
}
