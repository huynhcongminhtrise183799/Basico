using AccountService.Application.DTOs.Response;
using AccountService.Application.Queries.StaffQuery;
using AccountService.Domain.IRepositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler.StaffQueryHandler
{
	public class GetAllActiveStaffQueryHandler : IRequestHandler<GetAllActiveStaffQuery, List<StaffResponse>>
	{
		private readonly IAccountRepositoryRead _repoRead;

		public GetAllActiveStaffQueryHandler(IAccountRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task<List<StaffResponse>> Handle(GetAllActiveStaffQuery request, CancellationToken cancellationToken)
		{
			var staffs = await _repoRead.GetAllActiveStaff();

			var pagedStaffs = staffs
				.Skip((request.Page - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(s => new StaffResponse
				{
					StaffId = s.AccountId,
					FullName = s.AccountFullName,
					Email = s.AccountEmail,
					Gender = s.AccountGender
				})
				.ToList();

			return pagedStaffs;
		}
	}
}
