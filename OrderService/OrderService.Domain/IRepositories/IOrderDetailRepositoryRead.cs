using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.IRepositories
{
	public interface IOrderDetailRepositoryRead
	{
		Task<List<OrderDetail>> GetOrderDetailsByOrderIdAndCustomerId(Guid orderId, Guid customerId);
	}
}
