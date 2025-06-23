using AccountService.Application.DTOs.Response;
using AccountService.Application.Queries.Shift;
using AccountService.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler.Shift
{
	public class GetAllShiftHandler : IRequestHandler<GetAllShiftQuery, List<ShiftResponse>>
	{
		private readonly IShiftRepositoryRead _repoRead;
		public GetAllShiftHandler(IShiftRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}
		public async Task<List<ShiftResponse>> Handle(GetAllShiftQuery request, CancellationToken cancellationToken)
		{
			var shifts = await _repoRead.GetAll();
			var shiftResponses = shifts.Select(shift => new ShiftResponse
			{
				ShiftId = shift.ShiftId,
				StartTime = shift.StartTime,
				EndTime = shift.EndTime,

			}).ToList();
			return shiftResponses;
		}
	}
}
