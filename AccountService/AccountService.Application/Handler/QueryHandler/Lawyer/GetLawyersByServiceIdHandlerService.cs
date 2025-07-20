using AccountService.Application.DTOs.Response;
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
	public class GetLawyersByServiceIdHandlerService : IRequestHandler<GetLawyersByServiceIdQuery, List<LawyerWithServiceResponse>>
	{
		private readonly IAccountRepositoryRead _repository;
		public GetLawyersByServiceIdHandlerService(IAccountRepositoryRead repository)
		{
			_repository = repository;
		}
		public async Task<List<LawyerWithServiceResponse>> Handle(GetLawyersByServiceIdQuery request, CancellationToken cancellationToken)
		{
			var lawyers = await _repository.GetLaywersByServiceId(request.id);
			var lawyerResponses = lawyers.Select(lawyer => new LawyerWithServiceResponse
			{
				LawyerId = lawyer.AccountId,
				Email = lawyer.AccountEmail,
				FullName = lawyer.AccountFullName,
				Phone = lawyer.AccountPhone,
				Image = lawyer.AccountImage,
				PricePerHour = lawyer.LawyerSpecificServices
					.FirstOrDefault(s => s.ServiceId == request.id)?.PricePerHour ?? 0,
			}).ToList();
			return lawyerResponses;
		}
	}
}
