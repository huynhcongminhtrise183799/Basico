using AccountService.Application.Commands.Lawyer;
using AccountService.Application.Event.Lawyer;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using Cqrs.Events;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.Lawyer
{
    public class CreateLawyerHandler : IRequestHandler<CreateLawyerCommand, Guid>
    {
        private readonly IAccountRepositoryWrite _accountRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateLawyerHandler(IAccountRepositoryWrite accountRepository, IPublishEndpoint publishEndpoint)
        {
            _accountRepository = accountRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(CreateLawyerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Lawyer;
            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountUsername = dto.AccountUsername,
                AccountPassword = dto.AccountPassword,
                AccountEmail = dto.AccountEmail,
                AccountFullName = dto.AccountFullName,
                AccountDob = dto.AccountDob,
                AccountGender = dto.AccountGender,
                AccountPhone = dto.AccountPhone,
                AccountImage = dto.AccountImage,
                AboutLawyer = dto.AboutLawyer,
                AccountRole = Role.LAWYER.ToString(),
                AccountStatus = Status.ACTIVE.ToString(),
                AccountTicketRequest = 0
            };

            await _accountRepository.AddLawyerAsync(account, cancellationToken);
            await _accountRepository.SaveChangesAsync(cancellationToken);

            var @event = new LawyerCreatedEvent
            {
                AccountId = account.AccountId,
                AccountUsername = request.Lawyer.AccountUsername,
                AccountPassword = request.Lawyer.AccountPassword,
                AccountEmail = request.Lawyer.AccountEmail,
                AccountFullName = request.Lawyer.AccountFullName,
                AccountDob = request.Lawyer.AccountDob,
                AccountGender = request.Lawyer.AccountGender,
                AccountPhone = request.Lawyer.AccountPhone,
                AccountImage = request.Lawyer.AccountImage,
                AboutLawyer = request.Lawyer.AboutLawyer
            };

            await _publishEndpoint.Publish(@event, cancellationToken);

            return account.AccountId;

        }
    }
}
