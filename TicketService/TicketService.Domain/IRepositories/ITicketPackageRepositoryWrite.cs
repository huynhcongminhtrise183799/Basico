using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.Entities;

namespace TicketService.Domain.IRepositories
{
    public interface ITicketPackageRepositoryWrite
    {
        Task<bool> AddAsync(TicketPackage ticketPackage);
        Task<bool> UpdateAsync(TicketPackage ticketPackage);
        Task<bool> DeleteAsync(Guid ticketPackageId);

    }
}
