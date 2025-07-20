using AccountService.Application.Event.AccountEvent;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.AccountConsumers
{
    public class AccountRegisteredEventConsumerService : IConsumer<AccountRegisteredEvent>
    {
        private readonly IAccountRepositoryRead _repo;

        public AccountRegisteredEventConsumerService(IAccountRepositoryRead repo)
        {
            _repo = repo;
        }

        public async Task Consume(ConsumeContext<AccountRegisteredEvent> context)
        {
            var evt = context.Message;

            await _repo.AddAsync(new Account
            {
                AccountId = evt.AccountId,
                AccountUsername = evt.AccountUsername,
                AccountPassword = evt.AccountPassword, 
                AccountEmail = evt.AccountEmail,
                AccountFullName = evt.AccountFullName,
                AccountGender = evt.AccountGender,
                AccountRole = evt.AccountRole,
                AccountStatus = evt.AccountStatus,
                AccountTicketRequest = evt.AccountTicketRequest
            });
            
        }
    }
}
