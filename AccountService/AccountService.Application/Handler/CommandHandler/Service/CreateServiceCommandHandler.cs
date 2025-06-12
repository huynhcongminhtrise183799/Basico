
using AccountService.Application.Commands.Service;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using AccountService.Application.Event.Service;


namespace AccountService.Application.Handler.CommandHandler.Service
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Guid>
    {
        private readonly IAccountRepositoryWrite _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateServiceCommandHandler(IAccountRepositoryWrite repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = new Domain.Entity.Service
            {
                ServiceId = Guid.NewGuid(),
                ServiceName = request.ServiceName,
                ServiceDescription = request.ServiceDescription
            };

            await _repository.AddServiceAsync(service);

            await _publishEndpoint.Publish(new ServiceCreatedEvent
            {
                ServiceId = service.ServiceId,
                ServiceName = service.ServiceName,
                ServiceDescription = service.ServiceDescription
            }, cancellationToken);

            return service.ServiceId;
        }
    }
}
