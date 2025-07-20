using AccountService.Domain.IRepositories;
using Application.Services.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler.Service
{
    public class GetAllServicesQueryHandlerService : IRequestHandler<GetAllServicesQuery, IEnumerable<Domain.Entity.Service>>
    {
        private readonly IAccountRepositoryRead _repository;

        public GetAllServicesQueryHandlerService(IAccountRepositoryRead repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.Entity.Service>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllServiceAsync();
        }
    }
}
