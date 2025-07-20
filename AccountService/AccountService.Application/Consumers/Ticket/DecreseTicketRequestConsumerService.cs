using AccountService.Domain.IRepositories;
using Contracts.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.Ticket
{
    public class DecreseTicketRequestConsumerService : IConsumer<DecreseTicketRequestEvent>
    {
        private readonly IAccountRepositoryWrite _accountWriteRepo;
        private readonly IAccountRepositoryRead _accountReadRepo;

        public DecreseTicketRequestConsumerService(IAccountRepositoryWrite accountWriteRepo, IAccountRepositoryRead accountReadRepo)
        {
            _accountWriteRepo = accountWriteRepo;
            _accountReadRepo = accountReadRepo;
        }

        public async Task Consume(ConsumeContext<DecreseTicketRequestEvent> context)
        {
            var message = context.Message;
            var account = await _accountReadRepo.GetByUserIdAsync(message.CustomerId);
            account.AccountTicketRequest -= 1; // Decrease the ticket request by 1
            await _accountWriteRepo.UpdateAccount(account);
            await _accountReadRepo.UpdateAccount(account);

        }
    }
}
