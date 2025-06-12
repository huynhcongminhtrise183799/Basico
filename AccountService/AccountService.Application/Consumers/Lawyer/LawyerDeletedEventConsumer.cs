using AccountService.Application.Event.Lawyer;
using AccountService.Domain.Entity;
using AccountService.Infrastructure.Read;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.Lawyer
{
    public class LawyerDeletedEventConsumer : IConsumer<LawyerDeletedEvent>
    {
        private readonly AccountDbContextRead _dbContextRead;

        public LawyerDeletedEventConsumer(AccountDbContextRead dbContextRead)
        {
            _dbContextRead = dbContextRead;
        }

        public async Task Consume(ConsumeContext<LawyerDeletedEvent> context)
        {
            var evt = context.Message;
            var account = await _dbContextRead.Accounts.FindAsync(evt.AccountId);
            if (account == null) return;

            account.AccountStatus = Status.INACTIVE.ToString();
            await _dbContextRead.SaveChangesAsync();
        }
    }
}
