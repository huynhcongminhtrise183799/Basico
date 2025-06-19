using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.Entities;
using TicketService.Domain.IRepositories;

namespace TicketService.Infrastructure.Read.Repository
{
    public class TicketRepositoryRead : ITicketRepositoryRead
    {
        private readonly TicketDbContextRead _context;

        public TicketRepositoryRead(TicketDbContextRead context)
        {
            _context = context;
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<Ticket?> GetByIdAsync(Guid ticketId)
        {
            return await _context.Tickets.FindAsync(ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null)
                throw new KeyNotFoundException("Ticket not found for deletion.");

            ticket.Status = TicketStatus.CANCELLED.ToString();
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ticket>> GetByStatusAsync(string status)
        {
            return await _context.Tickets
                .Where(t => t.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetByCustomerIdAsync(Guid userId)
        {
            return await _context.Tickets
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task MarkAsCancelledAsync(Guid ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null)
                return;

            ticket.Status = TicketStatus.CANCELLED.ToString();
            await _context.SaveChangesAsync();
        }
    }

}
