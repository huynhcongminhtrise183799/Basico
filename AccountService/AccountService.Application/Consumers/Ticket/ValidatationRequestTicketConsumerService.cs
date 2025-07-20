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
    public class ValidatationRequestTicketConsumerService : IConsumer<ValidatationRequestTicket>
    {
        private readonly IAccountRepositoryRead _repoRead;

        public ValidatationRequestTicketConsumerService(IAccountRepositoryRead repoRead)
        {
            _repoRead = repoRead;
        }

        public async Task Consume(ConsumeContext<ValidatationRequestTicket> context)
        {
            var message = context.Message;
            var account = await _repoRead.GetByUserIdAsync(message.AccountId);
            var response = new ValidatationRequestTicket
            {
                AccountId = message.AccountId,
            };
            if (account.AccountTicketRequest > 0)
            {
                response.IsValid = true;
            }
            else
            {
                response.IsValid = false; // Set to false if the ticket request is not valid
            }
            await context.RespondAsync(response);
        }
    }
}
