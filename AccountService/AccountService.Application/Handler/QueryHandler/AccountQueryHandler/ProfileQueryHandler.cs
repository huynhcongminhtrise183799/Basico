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
    public class ProfileQueryHandler : IRequestHandler<ProfileQuery, ProfileResponse>
    {
        private readonly IAccountRepositoryRead _repoRead;

        public ProfileQueryHandler(IAccountRepositoryRead repoRead)
        {
            _repoRead = repoRead;
        }
        public async Task<ProfileResponse> Handle(ProfileQuery request, CancellationToken cancellationToken)
        {
            var account = await _repoRead.GetAccountById(request.accountId);
            if (account == null)
            {
                return null; // or throw an exception if preferred
            }
            return new ProfileResponse
            {
                AccountId = account.AccountId,
                Email = account.AccountEmail,
                FullName = account.AccountFullName,
                AccountTicketRequest = account.AccountTicketRequest,
                Gender = account.AccountGender,
				Username = account.AccountUsername
			};
        }
    }
}
