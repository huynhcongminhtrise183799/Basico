using AccountService.Application.Event.Lawyer;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using AccountService.Infrastructure.Read;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers
{
    public class LawyerCreatedEventConsumer : IConsumer<LawyerCreatedEvent>
    {
        private readonly IAccountRepositoryRead _accountRepository;

        // Fix for CS1520: Add return type 'void' to the constructor
        // Fix for IDE0290: Use primary constructor syntax
        public LawyerCreatedEventConsumer(IAccountRepositoryRead accountRepository) => _accountRepository = accountRepository;

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

            await _accountRepository.AddAsync(account);
        }
    }
}