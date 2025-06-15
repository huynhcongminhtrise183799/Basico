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
    public class GetLawyerByIdHandler : IRequestHandler<GetLawyerByIdQuery, LawyerDto>
    {
        private readonly IAccountRepositoryRead _accountRepository;

        public GetLawyerByIdHandler(IAccountRepositoryRead accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<LawyerDto> Handle(GetLawyerByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetLawyerByIdAsync(request.AccountId, cancellationToken);
            if (account == null || account.AccountRole != "LAWYER")
                return null;

            return new LawyerDto
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
			};
        }
    }
    }
