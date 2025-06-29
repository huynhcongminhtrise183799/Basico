using MediatR;
using OrderService.Application.Queries;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Handler.QueryHandler
{
	public class GetAllOrderHandler : IRequestHandler<GetAllOrderQuery, List<Order>>
	{
		private readonly IOrderRepositoryRead _repoRead;

		public GetAllOrderHandler(IOrderRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task<List<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
		{
			return await _repoRead.GetAllOrdersAsync();
		}
	}

}
