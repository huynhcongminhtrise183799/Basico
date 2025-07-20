using AccountService.Application.DTOs.Response;
using AccountService.Application.Queries.AccountQuery;
using AccountService.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler.AccountQueryHandler
{
	public class GetAllAccountQueryHandlerService : IRequestHandler<GetAllAccountQuery, List<AccountResponse>>
	{
		private readonly IAccountRepositoryRead _repoRead;

		public GetAllAccountQueryHandlerService(IAccountRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task<List<AccountResponse>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
		{
			var accounts = await _repoRead.GetAllUserAccounts();
			var accountResponses = accounts.Select(account => new AccountResponse
			{
				AccountId = account.AccountId,
				Email = account.AccountEmail,
				FullName = account.AccountFullName,
				PhoneNumber = account.AccountPhone,
				Image = account.AccountImage,
				Status = account.AccountStatus.ToString()
			}).ToList();

			return accountResponses;
		}
	}

}
