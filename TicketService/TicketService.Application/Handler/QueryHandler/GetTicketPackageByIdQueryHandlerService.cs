using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.DTOs.Response;
using TicketService.Application.Queries;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Handler.QueryHandler
{
    public class GetTicketPackageByIdQueryHandlerService : IRequestHandler<GetTicketPackageByIdQuery, TicketPackageResponse>
    {
        private readonly ITicketPackageRepositoryRead _repo;
        public GetTicketPackageByIdQueryHandlerService(ITicketPackageRepositoryRead repo)
        {
            _repo = repo;
        }
        public async Task<TicketPackageResponse> Handle(GetTicketPackageByIdQuery request, CancellationToken cancellationToken)
        {
            var package = await _repo.GetByIdAsync(request.id);
            if (package == null)
            {
                return null; // or throw an exception if preferred
            }
            var response = new TicketPackageResponse
            {
                TicketPackageId = package.TicketPackageId,
                TicketPackageName = package.TicketPackageName,
                RequestAmount = package.RequestAmount,
                Price = package.Price,
                Status = package.Status
            };
            return response;
        }
    }
}
