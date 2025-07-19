using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.Entities;
using TicketService.Domain.IRepositories;

namespace TicketService.Infrastructure.Write.Repository
{
    public class TicketRepositoryWrite : ITicketRepositoryWrite
    {
        private readonly TicketDbContextWrite _context;

        public TicketRepositoryWrite(TicketDbContextWrite context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Ticket ticket)
        {
            try
            {
				await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
				return true;
			}
            catch (Exception)
            {

               return false;
			}
        }

        public async Task<Ticket?> GetByIdAsync(Guid ticketId)
        {
            return await _context.Tickets.FindAsync(ticketId);
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
        }

        public async Task DeleteAsync(Guid ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket != null)
            {
                ticket.Status = TicketStatus.CANCELLED.ToString();
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ReplyTicket(Ticket ticket)
        {
            try
            {
				var existingTicket = await _context.Tickets.FindAsync(ticket.TicketId);
				if (existingTicket != null)
				{
					existingTicket.StaffId = ticket.StaffId;
					existingTicket.Content_Response = ticket.Content_Response;
					existingTicket.Status = ticket.Status;
					await _context.SaveChangesAsync();
					return true;
				}
                return false;

			}
            catch (Exception)
            {

                return false;
			}
        }
    }
}
