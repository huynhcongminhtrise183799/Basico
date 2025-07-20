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
	public class GetDayOffBetweenDayHandlerService : IRequestHandler<GetDayOffsBetween, List<DayOffResponse>>
	{
		private readonly ILawyerDayOffScheduleRepositoryRead _repoRead;
		private readonly IAccountRepositoryRead _accountRepoRead;

		public GetDayOffBetweenDayHandlerService(ILawyerDayOffScheduleRepositoryRead repoRead, IAccountRepositoryRead accountRepoRead)
		{
			_repoRead = repoRead;
			_accountRepoRead = accountRepoRead;
		}

		public async Task<List<DayOffResponse>> Handle(GetDayOffsBetween request, CancellationToken cancellationToken)
		{
			var dayOffEntities = await _repoRead.GetDayOffBetweenDay(request.fromDate, request.toDate);

			var result = dayOffEntities.Select(dayOff => new DayOffResponse
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
			}).ToList();

			return result;
		}

	}
}
