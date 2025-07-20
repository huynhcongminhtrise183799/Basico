using AccountService.Application.Event.Lawyer;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.Lawyer
{
    public class LawyerDeletedEventConsumerService : IConsumer<LawyerDeletedEvent>
    {
        private readonly IAccountRepositoryRead _repo;

        public LawyerDeletedEventConsumerService(IAccountRepositoryRead repo)
        {
            _repo = repo;
        }

        public async Task Consume(ConsumeContext<LawyerDeletedEvent> context)
        {
            var evt = context.Message;
            var account = await _repo.GetAccountById(evt.AccountId);
            if (account == null) return;

            account.AccountStatus = Status.INACTIVE.ToString();
           await _repo.UpdateAccount(account);

           
        }
    }
}
