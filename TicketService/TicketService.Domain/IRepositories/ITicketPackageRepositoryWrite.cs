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
        Task AddAsync(TicketPackage ticketPackage);
        Task UpdateAsync(TicketPackage ticketPackage);
        Task DeleteAsync(Guid ticketPackageId);

    }
}
