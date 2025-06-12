using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
    public interface IAccountRepositoryRead
    {
		Task<Account?> GetByEmailAsync(string email);
		Task AddAsync(Account account);
        Task<Account?> GetAccountByUserNameAndPassword(string username, string password);
        Task<Account?> GetAccountById(Guid accountId);
        Task<Account?> GetAccountByUserName(string username);
        //Lawyer
        Task<Account?> GetLawyerByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Account>> GetAllLawyersAsync(CancellationToken cancellationToken);
        Task<List<Account>> GetAllActiveLawyerAccountsAsync(CancellationToken cancellationToken);
        Task AddLawyerAsync(Account account, CancellationToken cancellationToken = default);
        Task UpdateLawyerAsync(Account account, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        //Service
        Task AddServiceAsync(Service service);
        Task UpdateServiceAsync(Service service);
        Task DeleteServiceAsync(Guid serviceId);
        Task<Service> GetServiceByIdAsync(Guid serviceId);
        Task<IEnumerable<Service>> GetAllServiceAsync();

    }
}
