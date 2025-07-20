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
    public class GetServiceByIdQueryHandlerService : IRequestHandler<GetServiceByIdQuery, Domain.Entity.Service>
    {
        private readonly IAccountRepositoryRead _repository;

        public GetServiceByIdQueryHandlerService(IAccountRepositoryRead repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entity.Service> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetServiceByIdAsync(request.ServiceId);
        }
    }
}
