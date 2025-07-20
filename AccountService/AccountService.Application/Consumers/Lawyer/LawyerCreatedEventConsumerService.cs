using AccountService.Application.Event.Lawyer;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers
{
    public class LawyerCreatedEventConsumerService : IConsumer<LawyerCreatedEvent>
    {
        private readonly IAccountRepositoryRead _accountRepository;
        private readonly ILawyerSpecificServiceRepositoryRead _lawyerSpecificServiceRepository;


        // Fix for CS1520: Add return type 'void' to the constructor
        // Fix for IDE0290: Use primary constructor syntax
        public LawyerCreatedEventConsumerService(IAccountRepositoryRead accountRepository, ILawyerSpecificServiceRepositoryRead lawyerSpecificServiceRepositoryRead )
        {
            _accountRepository = accountRepository;
            _lawyerSpecificServiceRepository = lawyerSpecificServiceRepositoryRead;

        }
        public async Task Consume(ConsumeContext<LawyerCreatedEvent> context)
        {
            var e = context.Message;

            var account = new Account
            {
                AccountId = e.AccountId,
                AccountUsername = e.AccountUsername,
                AccountPassword = e.AccountPassword,
                AccountEmail = e.AccountEmail,
                AccountFullName = e.AccountFullName,
                AccountDob = e.AccountDob,
                AccountGender = e.AccountGender,
                AccountPhone = e.AccountPhone,
                AccountImage = e.AccountImage,
                AboutLawyer = e.AboutLawyer,
                AccountRole = Role.LAWYER.ToString(),
                AccountStatus = Status.ACTIVE.ToString(),
                AccountTicketRequest = 0
            };

            var lawyerSpecificServices = e.ServiceForLawyerDTOs.Select(service => new LawyerSpecificService
            {
                LawyerId = e.AccountId,
                ServiceId = service.ServiceId,
                PricePerHour = service.PricePerHour
            }).ToList();


            await _accountRepository.AddAsync(account);
            await _lawyerSpecificServiceRepository.AddAsync(lawyerSpecificServices);
        }
    }
}