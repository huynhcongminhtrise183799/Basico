using AccountService.Domain.Entity;
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
    public class UpdateAccountTicketRequestConsumer : IConsumer<UpdateAccountTicketRequestEvent>
    {
        private readonly IAccountRepositoryWrite _accountWriteRepo;
        private readonly IAccountRepositoryRead _accountReadRepo;

        public UpdateAccountTicketRequestConsumer(
            IAccountRepositoryWrite accountWriteRepo,
            IAccountRepositoryRead accountReadRepo)
        {
            _accountWriteRepo = accountWriteRepo;
            _accountReadRepo = accountReadRepo;
        }

        public async Task Consume(ConsumeContext<UpdateAccountTicketRequestEvent> context)
        {
            var message = context.Message;

            var writeAccount = await _accountWriteRepo.GetByUserIdAsync(message.CustomerID, context.CancellationToken);
            if (writeAccount != null)
            {
                writeAccount.AccountTicketRequest += message.TicketRequestAmount;
                await _accountWriteRepo.UpdateAccount(writeAccount);
                await _accountWriteRepo.SaveChangesAsync(context.CancellationToken);
            }

            var readAccount = await _accountReadRepo.GetByUserIdAsync(message.CustomerID);
            if (readAccount != null)
            {
                readAccount.AccountTicketRequest += message.TicketRequestAmount;
                await _accountReadRepo.UpdateAccount(readAccount);
                await _accountReadRepo.SaveChangesAsync(context.CancellationToken);
            }
        }
    }
}
