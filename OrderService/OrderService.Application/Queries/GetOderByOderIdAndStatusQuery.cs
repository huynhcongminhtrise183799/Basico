using MediatR;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries
{
	public record GetOderByOderIdAndStatusQuery(Guid OrderId, string Status) : IRequest<Order>;

}
