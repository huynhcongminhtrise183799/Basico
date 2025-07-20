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
	public class GetCustomerFormByIdHandlerService : IRequestHandler<GetCustomerFormByIdQuery, CustomerForm>
	{
		private readonly IFormDataRepositoryRead _formDataRepositoryRead;

		public GetCustomerFormByIdHandlerService(IFormDataRepositoryRead formDataRepositoryRead)
		{
			_formDataRepositoryRead = formDataRepositoryRead;
		}

		public async Task<CustomerForm> Handle(GetCustomerFormByIdQuery request, CancellationToken cancellationToken)
		{
			return await _formDataRepositoryRead.GetCustomerFormByIdAsync(request.CustomerFormId);
		}
	}

}
