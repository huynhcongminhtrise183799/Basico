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
    public class GetAllTicketPackageQueryHandlerService : IRequestHandler<GetAllTicketPackageQuery, List<TicketPackageResponse>>
    {
        private readonly ITicketPackageRepositoryRead _repo;
        public GetAllTicketPackageQueryHandlerService(ITicketPackageRepositoryRead repo)
        {
            _repo = repo;
        }
        public async Task<List<TicketPackageResponse>> Handle(GetAllTicketPackageQuery request, CancellationToken cancellationToken)
        {
            var packages = await _repo.GetAllAsync();
            var response = packages.Select(p => new TicketPackageResponse
            {
                TicketPackageId = p.TicketPackageId,
                TicketPackageName = p.TicketPackageName,
                RequestAmount = p.RequestAmount,
                Price = p.Price,
                Status = p.Status
            }).ToList();
            return response;
        }
    }
}
