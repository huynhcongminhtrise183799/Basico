using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
    public interface IAccountRepositoryWrite
    {
        //Register
        Task AddAsync(Account account);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);
        //Lawyer
        Task AddLawyerAsync(Account account, CancellationToken cancellationToken);
        Task<Account?> GetLawyerByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateLawyerAsync(Account account, CancellationToken cancellationToken);
        Task DeleteLawyerAsync(Account account, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        //Service
        Task<Service> AddServiceAsync(Service service);
        Task UpdateServiceAsync(Service service);
        Task DeleteServiceAsync(Guid serviceId);
        Task<Service> GetServiceByIdAsync(Guid serviceId);
    }
}
