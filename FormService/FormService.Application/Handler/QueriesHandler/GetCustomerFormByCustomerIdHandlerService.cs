using FormService.Application.Queries;
using FormService.Domain.Entities;
using FormService.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Handler.QueriesHandler
{
	public class GetCustomerFormByCustomerIdHandlerService : IRequestHandler<GetCustomerFormByCustomerIdQuery, List<CustomerForm>>
	{
		private readonly IFormDataRepositoryRead _repoRead;
		public GetCustomerFormByCustomerIdHandlerService(IFormDataRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}
		public async Task<List<CustomerForm>> Handle(GetCustomerFormByCustomerIdQuery request, CancellationToken cancellationToken)
		{
			return await _repoRead.GetCustomerFormsByCustomerIdAsync(request.CustomerId);
		}
	}
}
