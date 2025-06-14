using AccountService.Application.Event.Lawyer;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using AccountService.Infrastructure.Read;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.Lawyer
{
    public class LawyerUpdatedEventConsumer : IConsumer<LawyerUpdatedEvent>
    {
        private readonly AccountDbContextRead _dbContextRead;
        private readonly ILawyerSpecificServiceRepositoryRead _lawyerSpecific;

        public LawyerUpdatedEventConsumer(AccountDbContextRead dbContextRead, ILawyerSpecificServiceRepositoryRead lawyerSpecific)
        {
            _dbContextRead = dbContextRead;
            _lawyerSpecific = lawyerSpecific;
        }

        public async Task Consume(ConsumeContext<LawyerUpdatedEvent> context)
        {
            var evt = context.Message;
            var account = await _dbContextRead.Accounts.FindAsync(evt.AccountId);
            if (account == null) return;

            account.AccountFullName = evt.AccountFullName;
            account.AccountDob = evt.AccountDob != default ? evt.AccountDob : account.AccountDob;
            account.AccountGender = evt.AccountGender;
            account.AccountPhone = evt.AccountPhone;
            account.AccountImage = evt.AccountImage;
            account.AboutLawyer = evt.AboutLawyer;

            _dbContextRead.Accounts.Update(account);
            await _dbContextRead.SaveChangesAsync();

            var newLawyerService = evt.ServiceForLawyerDTOs.Select(service => new LawyerSpecificService
            {
                LawyerId = account.AccountId,
                ServiceId = service.ServiceId,
                PricePerHour = service.PricePerHour,
            }).ToList();
           await _lawyerSpecific.UpdateAsync(newLawyerService,evt.AccountId);
        }
    }
}
