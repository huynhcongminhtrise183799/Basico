using AccountService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Queries.StaffQuery
{
	public class GetStaffByIdQuery : IRequest<StaffResponse?>
	{
		public Guid StaffId { get; }

		public GetStaffByIdQuery(Guid staffId)
		{
			StaffId = staffId;
		}
	}
}
