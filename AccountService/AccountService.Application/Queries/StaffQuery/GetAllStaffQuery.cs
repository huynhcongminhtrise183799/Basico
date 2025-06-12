using MediatR;
using System.Collections.Generic;
using AccountService.Application.DTOs.Response;

namespace AccountService.Application.Queries.StaffQuery
{
    public class GetAllStaffQuery : IRequest<List<StaffResponse>>
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}