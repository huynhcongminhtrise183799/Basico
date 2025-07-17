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
    public class SendEmailBookingConsumer : IConsumer<SendEmailBookingEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IAccountRepositoryRead _accountRepositoryRead;

        public async Task Consume(ConsumeContext<SendEmailBookingEvent> context)
        {
            var message = context.Message;
            var account = await _accountRepositoryRead.GetAccountById(message.AccountId.Value);

            var emailContent = $"success";

            await _emailService.SendAsync(account.AccountEmail, "Booking Confirmation", emailContent);
        }
    }
}
