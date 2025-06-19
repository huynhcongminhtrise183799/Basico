using AccountService.Application.DTOs.Response;
using AccountService.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Queries.Service
{
	public class GetAllServiceByStatusQuery : IRequest<List<ServiceResponseDTO>>
	{
		public string Status { get; set; }
	}
}
