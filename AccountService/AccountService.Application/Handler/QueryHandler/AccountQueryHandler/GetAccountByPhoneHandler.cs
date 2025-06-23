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
	public class GetAccountByPhoneHandler : IRequestHandler<GetAccountByPhoneQuery, AccountResponse>
	{
		private readonly IAccountRepositoryRead _accountRepositoryRead;

		public GetAccountByPhoneHandler(IAccountRepositoryRead accountRepositoryRead)
		{
			_accountRepositoryRead = accountRepositoryRead;
		}

		public async Task<AccountResponse> Handle(GetAccountByPhoneQuery request, CancellationToken cancellationToken)
		{
			var account = await _accountRepositoryRead.GetByPhoneAsync(request.Phone);
			if (account == null)
			{
				return null; // or throw an exception
			}

			return new AccountResponse
			{
				AccountId = account.AccountId,
				Email = account.AccountEmail,
				FullName = account.AccountFullName,
				PhoneNumber = account.AccountPhone,
				Image = account.AccountImage,
				Status = account.AccountStatus
			};
		}
	}

}
