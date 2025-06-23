using FormService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Queries
{
	public record GetCustomerFormByIdQuery(Guid CustomerFormId) : IRequest<CustomerForm>;


}
