using AccountService.Application.Commands.Lawyer;
using AccountService.Application.Event.Lawyer;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.Lawyer
{
    public class DeleteLawyerHandler : IRequestHandler<DeleteLawyerCommand>
    {
        private readonly IAccountRepositoryWrite _accountRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteLawyerHandler(IAccountRepositoryWrite accountRepository, IPublishEndpoint publishEndpoint)
        {
            _accountRepository = accountRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(DeleteLawyerCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetLawyerByIdAsync(request.AccountId, cancellationToken);
            if (account == null || account.AccountRole != "LAWYER")
                throw new System.Exception("Lawyer not found");

            account.AccountStatus = Status.INACTIVE.ToString();

            await _accountRepository.UpdateLawyerAsync(account, cancellationToken);
            await _accountRepository.SaveChangesAsync(cancellationToken);

            var @event = new LawyerDeletedEvent
            {
                AccountId = account.AccountId
            };

            await _publishEndpoint.Publish(@event, cancellationToken);

            return Unit.Value;
        }

        Task IRequestHandler<DeleteLawyerCommand>.Handle(DeleteLawyerCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
