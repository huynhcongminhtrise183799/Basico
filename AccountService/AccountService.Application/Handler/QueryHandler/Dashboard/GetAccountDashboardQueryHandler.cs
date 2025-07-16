using AccountService.Application.DTOs.Response;
using AccountService.Application.Queries.Dashboard;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler.Dashboard
{
	public class GetAccountDashboardQueryHandler : IRequestHandler<GetAccountDashboardQuery, AccountDashboardResponse>
	{
		private readonly IAccountRepositoryRead _repository;

		public GetAccountDashboardQueryHandler(IAccountRepositoryRead repository)
		{
			_repository = repository;
		}

		public async Task<AccountDashboardResponse> Handle(GetAccountDashboardQuery request, CancellationToken cancellationToken)
		{
			var allAccountsTask = await _repository.GetAllAccounts();
			var allUserAccountsTask = await _repository.GetAllUserAccounts();
			var allLawyerAccountsTask = await _repository.GetAllLawyersAsync(cancellationToken);
			var allStaffAccountsTask = await _repository.GetAllStaff();
			var allInactiveAccountsTask = allAccountsTask.Where(x => x.AccountStatus.ToUpper() == Status.INACTIVE.ToString().ToUpper()).ToList();
			var allInactiveUserAccountsTask = allUserAccountsTask.Where(x => x.AccountStatus.ToUpper() == Status.INACTIVE.ToString().ToUpper()).ToList();
			var allInactiveLawyerAccountsTask = allLawyerAccountsTask.Where(x => x.AccountStatus.ToUpper() == Status.INACTIVE.ToString().ToUpper()).ToList();
			var allInactiveStaffAccountsTask = allStaffAccountsTask.Where(x => x.AccountStatus.ToUpper() == Status.INACTIVE.ToString().ToUpper()).ToList();
			return new AccountDashboardResponse
			{
				AllAccounts = allAccountsTask.Count,
				AllUserAcccounts = allUserAccountsTask.Count,
				AllLawyerAccounts = allLawyerAccountsTask.Count(),
				AllStaffAccounts = allStaffAccountsTask.Count(),
				AllInactiveAccounts = allInactiveAccountsTask.Count,
				AllInactiveUserAccounts = allInactiveUserAccountsTask.Count,
				AllInactiveLawyerAccounts = allInactiveLawyerAccountsTask.Count,
				AllInactiveStaffAccounts = allInactiveStaffAccountsTask.Count
			};
		}

	}
}
