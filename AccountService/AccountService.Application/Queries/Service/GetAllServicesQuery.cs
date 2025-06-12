using MediatR;
using AccountService.Domain.Entity;

namespace Application.Services.Queries
{
    public class GetAllServicesQuery : IRequest<IEnumerable<Service>> { }
}