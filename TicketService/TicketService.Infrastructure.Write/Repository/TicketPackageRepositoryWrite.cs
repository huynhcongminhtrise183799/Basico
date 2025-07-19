using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.Entities;
using TicketService.Domain.IRepositories;

namespace TicketService.Infrastructure.Write.Repository
{
    public class TicketPackageRepositoryWrite : ITicketPackageRepositoryWrite
    {
        private readonly TicketDbContextWrite _context;
        public TicketPackageRepositoryWrite(TicketDbContextWrite context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(TicketPackage ticketPackage)
        {
            try
            {
				await _context.TicketPackages.AddAsync(ticketPackage);
				await _context.SaveChangesAsync();
                return true;
			}
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid ticketPackageId)
        {
            var ticketPackage = await _context.TicketPackages.FindAsync(ticketPackageId);
            if (ticketPackage == null)
            {
				throw new KeyNotFoundException("Ticket package not found for deletion.");
			}
                

            ticketPackage.Status = Status.INACTIVE.ToString();
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(TicketPackage ticketPackage)
        {
           var existingPackage = _context.TicketPackages.Find(ticketPackage.TicketPackageId);
            if (existingPackage != null)
            {
                existingPackage.TicketPackageName = ticketPackage.TicketPackageName;
                existingPackage.RequestAmount = ticketPackage.RequestAmount;
                existingPackage.Price = ticketPackage.Price;
                existingPackage.Status = ticketPackage.Status;
                await _context.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException("Ticket package not found.");
        }
    }
}
