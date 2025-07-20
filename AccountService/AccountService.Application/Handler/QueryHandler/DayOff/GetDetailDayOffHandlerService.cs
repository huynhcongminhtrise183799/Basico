using AccountService.Application.DTOs.Response;
using AccountService.Application.Queries.DayOff;
using AccountService.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler.DayOff
{
	public class GetDetailDayOffHandlerService : IRequestHandler<GetDetailDayOffQuery, DayOffResponse>
	{
		private readonly ILawyerDayOffScheduleRepositoryRead _repoRead;

		public GetDetailDayOffHandlerService(ILawyerDayOffScheduleRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task<DayOffResponse> Handle(GetDetailDayOffQuery request, CancellationToken cancellationToken)
		{
			var dayOff = await _repoRead.GetDayOffById(request.lawyerDayOffScheduleId);
			var result = new DayOffResponse
			{
				DayOffId = dayOff.LawyerDayOffScheduleId,
				LawyerId = dayOff.LawyerId,
				LawyerName = dayOff.Lawyer.AccountFullName ?? "Unknown", // nếu bạn có FullName trong Account
				DayOff = dayOff.OffDay,
				SpecificDayOffs = dayOff.SpecificLawyerDayOffSchedules.Select(s => new SpecificDayOffResponse
				{
					ShiftId = s.ShiftId,
					FromTime = s.Shift?.StartTime ?? default,
					ToTime = s.Shift?.EndTime ?? default,
					Status = s.Status
				}).ToList()
			};

			return result;
		}
	}
}
