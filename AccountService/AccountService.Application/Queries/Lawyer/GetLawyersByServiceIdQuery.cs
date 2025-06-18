using AccountService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Queries.Lawyer
{
	public  record GetLawyersByServiceIdQuery(Guid id ) : IRequest<List<LawyerWithServiceResponse>>;

}
