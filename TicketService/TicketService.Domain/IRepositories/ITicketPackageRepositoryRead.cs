using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.Entities;

namespace TicketService.Domain.IRepositories
{
    public interface ITicketPackageRepositoryRead
    {
        Task<TicketPackage?> GetByIdAsync(Guid ticketPackageId);
        Task<List<TicketPackage>> GetAllAsync();

        Task<List<TicketPackage>> GetAllActiveAsync();

        Task<List<TicketPackage>> GetAllTicketPackagePagition(int page);

        Task<List<TicketPackage>> GetByNameAsync(string ticketPackageName);
        Task AddAsync(TicketPackage ticketPackage);
        Task UpdateAsync(TicketPackage ticketPackage);
        Task DeleteAsync(Guid ticketPackageId);
    }
}
