using AccountService.Application.Event.AccountEvent;
using AccountService.Domain.Entity;
using AccountService.Infrastructure.Read;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.AccountConsumers
{
    public class AccountRegisteredEventConsumer : IConsumer<AccountRegisteredEvent>
    {
        private readonly AccountDbContextRead _context;

        public AccountRegisteredEventConsumer(AccountDbContextRead context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<AccountRegisteredEvent> context)
        {
            var evt = context.Message;

            _context.Accounts.Add(new Account
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
            await _context.SaveChangesAsync();
        }
    }
}
