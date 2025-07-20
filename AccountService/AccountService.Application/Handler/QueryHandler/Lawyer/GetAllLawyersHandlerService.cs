using AccountService.Application.DTOs.LawyerDTO;
using AccountService.Application.Queries.Lawyer;
using AccountService.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler.Lawyer
{
    public class GetAllLawyersHandlerService : IRequestHandler<GetAllLawyersQuery, IEnumerable<LawyerDto>>
    {
        private readonly IAccountRepositoryRead _accountRepository;

        public GetAllLawyersHandlerService(IAccountRepositoryRead accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<LawyerDto>> Handle(GetAllLawyersQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetAllLawyersAsync(cancellationToken);
            return accounts.Select(account => new LawyerDto
            {
                AccountId = account.AccountId,
                AccountUsername = account.AccountUsername,
                AccountEmail = account.AccountEmail,
                AccountFullName = account.AccountFullName,
                AccountDob = account.AccountDob,
                AccountGender = account.AccountGender,
                AccountPhone = account.AccountPhone,
                AccountImage = account.AccountImage,
                AboutLawyer = account.AboutLawyer,
                AccountStatus = account.AccountStatus,
				ServiceForLawyer = account.LawyerSpecificServices.Select(service => new ServiceForLawyerDTO
				{
					ServiceId = service.ServiceId,
                    PricePerHour = service.PricePerHour,
				}).ToList()
			});
        }
    }
}
