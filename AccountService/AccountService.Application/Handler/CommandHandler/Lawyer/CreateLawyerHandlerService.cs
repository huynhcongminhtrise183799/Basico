using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountService.Application.Commands.Lawyer;
using AccountService.Application.DTOs.Request;
using AccountService.Application.Event.Lawyer;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.Lawyer
{
    public class CreateLawyerHandlerService : IRequestHandler<CreateLawyerCommand, Guid>
    {
        private readonly IAccountRepositoryWrite _accountRepository;
        private readonly ILawyerSpecificServiceRepositoryWrite _lawyerSpecificService;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateLawyerHandlerService(IAccountRepositoryWrite accountRepository, IPublishEndpoint publishEndpoint, ILawyerSpecificServiceRepositoryWrite lawyerSpecificService)
        {
            _accountRepository = accountRepository;
            _publishEndpoint = publishEndpoint;
            _lawyerSpecificService = lawyerSpecificService;
        }

        public async Task<Guid> Handle(CreateLawyerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Lawyer;
			PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

			var account = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountUsername = dto.AccountUsername,
				
				AccountPassword = _passwordHasher.HashPassword(null, dto.AccountPassword),
                AccountEmail = dto.AccountEmail,
                AccountFullName = dto.AccountFullName,
                AccountDob = dto.AccountDob,
                AccountGender = dto.AccountGender,
                AccountPhone = dto.AccountPhone,
                AccountImage = dto.AccountImage,
                AboutLawyer = dto.AboutLawyer,
                AccountRole = Role.LAWYER.ToString(),
                AccountStatus = Status.ACTIVE.ToString()
            };

                var draf = dto.serviceForLawyerDTOs.Select(service => new LawyerSpecificService
                {
                    LawyerId = account.AccountId,
                    ServiceId = service.ServiceId,
                    PricePerHour = service.PricePerHour,
                }).ToList();
                
                



           var result_AddLawyer =  await _accountRepository.AddLawyerAsync(account, cancellationToken);
           var result_AddLawyerService =  await _lawyerSpecificService.AddAsync(draf);
			if (result_AddLawyer == null || result_AddLawyerService == null)
			{
				return Guid.Empty; // Failed to create lawyer
			}

			var @event = new LawyerCreatedEvent
            {
                AccountId = account.AccountId,
                AccountUsername = request.Lawyer.AccountUsername,
                AccountPassword = _passwordHasher.HashPassword(null, dto.AccountPassword),
                AccountEmail = request.Lawyer.AccountEmail,
                AccountFullName = request.Lawyer.AccountFullName,
                AccountDob = request.Lawyer.AccountDob,
                AccountGender = request.Lawyer.AccountGender,
                AccountPhone = request.Lawyer.AccountPhone,
                AccountImage = request.Lawyer.AccountImage,
                AboutLawyer = request.Lawyer.AboutLawyer,
                ServiceForLawyerDTOs = request.Lawyer.serviceForLawyerDTOs.Select(service => new LawyerSpecificServiceDTO
                {
                    LawyerId = account.AccountId,
                    ServiceId = service.ServiceId,
                    PricePerHour = service.PricePerHour
                }).ToList()
            };

            await _publishEndpoint.Publish(@event, cancellationToken);

            return account.AccountId;

        }
    }
}
