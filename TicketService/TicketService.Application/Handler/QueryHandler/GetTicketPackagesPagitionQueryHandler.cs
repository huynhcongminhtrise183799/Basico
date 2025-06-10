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
    public class GetTicketPackagesPagitionQueryHandler : IRequestHandler<GetTicketPackagesPagitionQuery, List<TicketPackageResponse>>
    {
        private readonly ITicketPackageRepositoryRead _repo;
        public GetTicketPackagesPagitionQueryHandler(ITicketPackageRepositoryRead repo)
        {
            _repo = repo;
        }
        public async Task<List<TicketPackageResponse>> Handle(GetTicketPackagesPagitionQuery request, CancellationToken cancellationToken)
        {
            var packages = await _repo.GetAllTicketPackagePagition(request.page);
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
