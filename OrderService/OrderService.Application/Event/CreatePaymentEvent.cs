using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Event
{
	public record CreatePaymentEvent (Guid PaymentId, Guid? OrderId, Guid? BookingId, double Amount, DateTime PaymentDate, string Status, string Method);
}
