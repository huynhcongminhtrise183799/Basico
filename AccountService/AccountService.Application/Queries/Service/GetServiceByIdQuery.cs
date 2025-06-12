using MediatR;
using AccountService.Domain.Entity;

namespace Application.Services.Queries
{
    public class GetServiceByIdQuery : IRequest<Service>
    {
        public Guid ServiceId { get; set; }
    }
}