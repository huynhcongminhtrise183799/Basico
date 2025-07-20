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
	public class GetOderByOderIdAndStatusHandlerService : IRequestHandler<GetOderByOderIdAndStatusQuery, Order?>
	{
		private readonly IOrderRepositoryRead _repoRead;

		public GetOderByOderIdAndStatusHandlerService(IOrderRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task<Order?> Handle(GetOderByOderIdAndStatusQuery request, CancellationToken cancellationToken)
		{
			return await _repoRead.GetOrderByIdAndStatusAsync(request.OrderId, request.Status);
		}
	}

}
