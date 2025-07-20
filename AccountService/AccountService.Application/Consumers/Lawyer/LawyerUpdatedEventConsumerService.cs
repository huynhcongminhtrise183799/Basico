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
    public class LawyerUpdatedEventConsumerService : IConsumer<LawyerUpdatedEvent>
    {
        private readonly IAccountRepositoryRead _repo;
        private readonly ILawyerSpecificServiceRepositoryRead _lawyerSpecific;

        public LawyerUpdatedEventConsumerService(IAccountRepositoryRead repo, ILawyerSpecificServiceRepositoryRead lawyerSpecific)
        {
            _repo = repo;
            _lawyerSpecific = lawyerSpecific;
        }

        public async Task Consume(ConsumeContext<LawyerUpdatedEvent> context)
        {
            var evt = context.Message;
            var account = await _repo.GetAccountById(evt.AccountId);
            if (account == null) return;

            account.AccountFullName = evt.AccountFullName;
            account.AccountDob = evt.AccountDob != default ? evt.AccountDob : account.AccountDob;
            account.AccountGender = evt.AccountGender;
            account.AccountPhone = evt.AccountPhone;
            account.AccountImage = evt.AccountImage;
            account.AboutLawyer = evt.AboutLawyer;

           await _repo.UpdateAccount(account);

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
