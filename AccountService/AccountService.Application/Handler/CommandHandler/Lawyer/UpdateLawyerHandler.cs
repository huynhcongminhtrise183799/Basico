using AccountService.Application.Commands.Lawyer;
using AccountService.Application.Event.Lawyer;
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
    public class UpdateLawyerHandler : IRequestHandler<UpdateLawyerCommand, Unit>
    {
        private readonly IAccountRepositoryWrite _accountRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateLawyerHandler(IAccountRepositoryWrite accountRepository, IPublishEndpoint publishEndpoint)
        {
            _accountRepository = accountRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(UpdateLawyerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Lawyer;
            var account = await _accountRepository.GetLawyerByIdAsync(dto.AccountId, cancellationToken);
            if (account == null || account.AccountRole != "LAWYER")
                throw new System.Exception("Lawyer not found");

            account.AccountFullName = dto.AccountFullName;
            account.AccountDob = dto.AccountDob != default ? dto.AccountDob : account.AccountDob; 
            account.AccountGender = dto.AccountGender;
            account.AccountPhone = dto.AccountPhone;
            account.AccountImage = dto.AccountImage;
            account.AboutLawyer = dto.AboutLawyer;

            await _accountRepository.UpdateLawyerAsync(account, cancellationToken);
            await _accountRepository.SaveChangesAsync(cancellationToken);

            var @event = new LawyerUpdatedEvent
            {
                AccountId = dto.AccountId,
                AccountFullName = dto.AccountFullName,
                AccountDob = dto.AccountDob,
                AccountGender = dto.AccountGender,
                AccountPhone = dto.AccountPhone,
                AccountImage = dto.AccountImage,
                AboutLawyer = dto.AboutLawyer
            };

            await _publishEndpoint.Publish(@event, cancellationToken);

            return Unit.Value;
        }
    }
}
