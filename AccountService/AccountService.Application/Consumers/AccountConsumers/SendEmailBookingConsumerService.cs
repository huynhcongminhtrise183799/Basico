using AccountService.Application.IService;
using AccountService.Domain.IRepositories;
using Contracts.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.AccountConsumers
{
    public class SendEmailBookingConsumerService : IConsumer<SendEmailBookingEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IAccountRepositoryRead _accountRepositoryRead;

        public SendEmailBookingConsumerService(IEmailService emailService, IAccountRepositoryRead accountRepositoryRead)
        {
            _emailService = emailService;
            _accountRepositoryRead = accountRepositoryRead;
        }

        public async Task Consume(ConsumeContext<SendEmailBookingEvent> context)
        {
            var message = context.Message;
            var accountId = message.AccountId.Value;
            var account = await _accountRepositoryRead.GetAccountById(accountId);

            var emailContent = $"success";

            await _emailService.SendAsync(account.AccountEmail, "Booking Confirmation", emailContent);
        }
    }
}
