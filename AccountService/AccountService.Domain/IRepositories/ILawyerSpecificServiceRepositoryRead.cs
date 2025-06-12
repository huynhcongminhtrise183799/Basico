using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
    public interface ILawyerSpecificServiceRepositoryRead
    {
        Task AddAsync(List<LawyerSpecificService> lawyerSpecificService);

        Task UpdateAsync(List<LawyerSpecificService> lawyerSpecificService, Guid lawyerId);

        Task<List<LawyerSpecificService>> GetLawyerSpecificServicesAsync(Guid lawyerId, CancellationToken cancellationToken);

    }
}
