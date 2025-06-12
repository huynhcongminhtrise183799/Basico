using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Read.Repository
{
    public class LawyerSpecificServiceRepositoryRead : ILawyerSpecificServiceRepositoryRead
    {
        private readonly AccountDbContextRead _context;
        public LawyerSpecificServiceRepositoryRead(AccountDbContextRead context)
        {
            _context = context;
        }
        public async Task AddAsync(List<LawyerSpecificService> lawyerSpecificService)
        {
          await _context.LawyerSpecificServices.AddRangeAsync(lawyerSpecificService);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LawyerSpecificService>> GetLawyerSpecificServicesAsync(Guid lawyerId, CancellationToken cancellationToken)
        {
            return await _context.LawyerSpecificServices
                .Where(x => x.LawyerId == lawyerId)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(List<LawyerSpecificService> lawyerSpecificService, Guid lawyerId)
        {
            // Lấy danh sách các service đã tồn tại của luật sư theo lawyerId
            var existingServices = _context.LawyerSpecificServices
                 .Where(x => x.LawyerId == lawyerId)
                 .ToList();

            // Xóa các service hiện có
            _context.LawyerSpecificServices.RemoveRange(existingServices);

            // Thêm các service mới vào context
            await _context.LawyerSpecificServices.AddRangeAsync(lawyerSpecificService);

            await _context.SaveChangesAsync();
        }
    }
}
