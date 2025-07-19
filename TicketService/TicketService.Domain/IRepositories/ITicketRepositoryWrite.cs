using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.Entities;

namespace TicketService.Domain.IRepositories
{
    public interface ITicketRepositoryWrite
    {
        Task<bool> AddAsync(Ticket ticket);                       
        Task<Ticket?> GetByIdAsync(Guid ticketId);         
        Task UpdateAsync(Ticket ticket);                  
        Task DeleteAsync(Guid ticketId);                   
        Task SaveChangesAsync();

        Task<bool> ReplyTicket(Ticket ticket);
    }
}
