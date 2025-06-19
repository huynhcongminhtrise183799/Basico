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
        Task UpdateAccount(Account account);

        // Staff
        Task AddStaff(Account staff);
        Task<bool> UpdateStaff(Account staff);
        Task<bool> DeleteStaff(Guid staffId);
        Task<IEnumerable<Account>> GetAllStaff();
        Task<Account?> GetStaffById(Guid staffId);
        Task<IEnumerable<Account>> GetAllActiveStaff();

        // Forgot password
        Task SaveOtpAsync(Guid accountId, string otp, DateTime expiredAt, Guid forgetPasswordId);

        // Lawyer
        Task<Account?> GetLawyerByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Account>> GetAllLawyersAsync(CancellationToken cancellationToken);
        Task<List<Account>> GetAllActiveLawyerAccountsAsync(CancellationToken cancellationToken);
        Task AddLawyerAsync(Account account, CancellationToken cancellationToken = default);
        Task UpdateLawyerAsync(Account account, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<List<Account>> GetLaywersByServiceId(Guid serviceId);

        //Service
        Task AddServiceAsync(Service service);
        Task UpdateServiceAsync(Service service);
        Task DeleteServiceAsync(Guid serviceId);
        Task<Service> GetServiceByIdAsync(Guid serviceId);
        Task<IEnumerable<Service>> GetAllServiceAsync();

        Task<string?> GetLaywerNameByLawyerId(Guid id);

        Task<string?> GetCustomerNameByCustomerId(Guid? customerId);

	
        // Ticket request
        Task UpdateAccountTicketRequestAsync(Guid userId, Guid ticketPackageId, int accountTicketRequest);
        Task<Account?> GetByUserIdAsync(Guid userId);
    }
}
