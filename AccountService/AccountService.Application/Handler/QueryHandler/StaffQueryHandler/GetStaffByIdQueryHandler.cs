using AccountService.Application.DTOs.Response;
using AccountService.Application.Queries.StaffQuery;
using AccountService.Domain.IRepositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler.StaffQueryHandler
{
	public class GetStaffByIdQueryHandler : IRequestHandler<GetStaffByIdQuery, StaffResponse?>
	{
		private readonly IAccountRepositoryRead _repoRead;

		public GetStaffByIdQueryHandler(IAccountRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task<StaffResponse?> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
		{
			var staff = await _repoRead.GetStaffById(request.StaffId);
			if (staff == null) return null;

			return new StaffResponse
			{
				StaffId = staff.AccountId,
				FullName = staff.AccountFullName,
				Email = staff.AccountEmail,
				Gender = staff.AccountGender
			};
		}
	}
}
