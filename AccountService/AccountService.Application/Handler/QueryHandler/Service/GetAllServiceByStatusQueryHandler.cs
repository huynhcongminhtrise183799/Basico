using AccountService.Application.DTOs.Response;
using AccountService.Application.Queries.Service;
using AccountService.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler.Service
{
    public class GetAllServiceByStatusQueryHandler : IRequestHandler<GetAllServiceByStatusQuery, List<ServiceResponseDTO>>
	{
		private readonly IServiceRepositoryRead _repository;

		public GetAllServiceByStatusQueryHandler(IServiceRepositoryRead repository)
		{
			_repository = repository;
		}

		public async Task<List<ServiceResponseDTO>> Handle(GetAllServiceByStatusQuery request, CancellationToken cancellationToken)
		{
			var services = await _repository.GetServicesByStatusAsync(request.Status);
			return services.Select(s => new ServiceResponseDTO
			{
				ServiceId = s.ServiceId,
				ServiceName = s.ServiceName,
				ServiceDescription = s.ServiceDescription
			}).ToList();
		}
	}

}
