using AccountService.Application.Queries.Lawyer;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler.Lawyer
{
    public class GetAllActiveLawyerAccountsHandler : IRequestHandler<GetAllActiveLawyerAccountsQuery, List<Account>>
    {
        private readonly IAccountRepositoryRead _repository;

        public GetAllActiveLawyerAccountsHandler(IAccountRepositoryRead repository)
        {
            _repository = repository;
        }

        public async Task<List<Account>> Handle(GetAllActiveLawyerAccountsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllActiveLawyerAccountsAsync(cancellationToken);
        }
    }
}
