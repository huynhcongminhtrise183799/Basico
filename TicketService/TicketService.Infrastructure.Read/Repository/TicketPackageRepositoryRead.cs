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
    public class TicketPackageRepositoryRead : ITicketPackageRepositoryRead
    {
        private readonly TicketDbContextRead _context;
        private const int PageSize = 1; // Example page size, can be adjusted
        public TicketPackageRepositoryRead(TicketDbContextRead context)
        {
            _context = context;
        }
        public async Task AddAsync(TicketPackage ticketPackage)
        {
           await _context.TicketPackages.AddAsync(ticketPackage);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid ticketPackageId)
        {
            var ticketPackage = _context.TicketPackages.Find(ticketPackageId);
            if (ticketPackage != null)
            {
                ticketPackage.Status = Status.INACTIVE.ToString(); // Soft delete
                return _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException("Ticket package not found for deletion.");
        }

        public Task<List<TicketPackage>> GetAllActiveAsync()
        {
            return _context.TicketPackages
                .Where(tp => tp.Status == Status.ACTIVE.ToString())
                .ToListAsync();
        }

        public async Task<List<TicketPackage>> GetAllAsync()
        {
           return await _context.TicketPackages
                .ToListAsync();
        }

        public async Task<List<TicketPackage>> GetAllTicketPackagePagition(int page)
        {
            return await _context.TicketPackages
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

        public async Task<TicketPackage?> GetByIdAsync(Guid ticketPackageId)
        {
           return await _context.TicketPackages
                .FirstOrDefaultAsync(tp => tp.TicketPackageId == ticketPackageId);
        }

        public Task<List<TicketPackage>> GetByNameAsync(string ticketPackageName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TicketPackage ticketPackage)
        {
            var existingPackage = _context.TicketPackages.Find(ticketPackage.TicketPackageId);
            if (existingPackage != null)
            {
                existingPackage.TicketPackageName = ticketPackage.TicketPackageName;
                existingPackage.RequestAmount = ticketPackage.RequestAmount;
                existingPackage.Price = ticketPackage.Price;
                existingPackage.Status = ticketPackage.Status;
                return _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException("Ticket package not found.");
        }
    }
}
