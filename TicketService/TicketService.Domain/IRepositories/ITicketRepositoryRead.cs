
using TicketService.Domain.Entities;

namespace TicketService.Domain.IRepositories
{
    public interface ITicketRepositoryRead
    {
        Task AddAsync(Ticket ticket);
        Task<Ticket?> GetByIdAsync(Guid ticketId);
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<IEnumerable<Ticket>> GetByStatusAsync(string status);
        Task<IEnumerable<Ticket>> GetByCustomerIdAsync(Guid userId);
        Task UpdateAsync(Ticket ticket);
        Task DeleteAsync(Guid ticketId);
        Task MarkAsCancelledAsync(Guid ticketId);
    }
}
