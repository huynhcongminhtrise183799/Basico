using AccountService.Application.Commands.Lawyer;
using AccountService.Application.DTOs.Request;
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
    public class UpdateLawyerHandlerService : IRequestHandler<UpdateLawyerCommand, Unit>
    {
        private readonly IAccountRepositoryWrite _accountRepository;
        private readonly ILawyerSpecificServiceRepositoryWrite _lawyerSpecificServiceRepositoryWrite;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateLawyerHandlerService(IAccountRepositoryWrite accountRepository, IPublishEndpoint publishEndpoint, ILawyerSpecificServiceRepositoryWrite lawyerSpecificServiceRepositoryWrite)
        {
            _accountRepository = accountRepository;
            _publishEndpoint = publishEndpoint;
            _lawyerSpecificServiceRepositoryWrite = lawyerSpecificServiceRepositoryWrite;
        }

        public async Task<Unit> Handle(UpdateLawyerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Lawyer;
            var account = await _accountRepository.GetLawyerByIdAsync(dto.AccountId, cancellationToken);
            if (account == null || account.AccountRole != "LAWYER")
                throw new Exception("Lawyer not found");

            account.AccountFullName = dto.AccountFullName;
            account.AccountDob = dto.AccountDob != default ? dto.AccountDob : account.AccountDob; 
            account.AccountGender = dto.AccountGender;
            account.AccountPhone = dto.AccountPhone;
            account.AccountImage = dto.AccountImage;
            account.AboutLawyer = dto.AboutLawyer;

            await _accountRepository.UpdateLawyerAsync(account, cancellationToken);
            await _accountRepository.SaveChangesAsync(cancellationToken);

            var newLawyerService =  dto.ServiceForLawyer.Select(service => new LawyerSpecificService
            {
                LawyerId = account.AccountId,
                ServiceId = service.ServiceId,
                PricePerHour = service.PricePerHour
            }).ToList();

            await _lawyerSpecificServiceRepositoryWrite.UpdateAsync(newLawyerService, dto.AccountId);


            var @event = new LawyerUpdatedEvent
            {
                AccountId = dto.AccountId,
                AccountFullName = dto.AccountFullName,
                AccountDob = dto.AccountDob,
                AccountGender = dto.AccountGender,
                AccountPhone = dto.AccountPhone,
                AccountImage = dto.AccountImage,
                AboutLawyer = dto.AboutLawyer,
                ServiceForLawyerDTOs = request.Lawyer.ServiceForLawyer.Select(service => new LawyerSpecificServiceDTO
                {
                    LawyerId = account.AccountId,
                    ServiceId = service.ServiceId,
                    PricePerHour = service.PricePerHour
                }).ToList()
            };

            await _publishEndpoint.Publish(@event, cancellationToken);

            return Unit.Value;
        }
    }
}
