using AccountService.Application.DTOs.Response;
using MediatR;
using System.Collections.Generic;

namespace AccountService.Application.Queries.StaffQuery
{
	public class GetAllActiveStaffQuery : IRequest<List<StaffResponse>>
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}
