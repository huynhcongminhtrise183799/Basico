﻿using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Write.Repository
{
    public class LawyerSpecificServiceRepositoryWrite : ILawyerSpecificServiceRepositoryWrite
    {
        private readonly AccountDbContextWrite _context;

        public LawyerSpecificServiceRepositoryWrite(AccountDbContextWrite context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(List<LawyerSpecificService> lawyerSpecificService)
        {
            try
            {
				await _context.LawyerSpecificServices.AddRangeAsync(lawyerSpecificService);
				await _context.SaveChangesAsync();
                return true;
			}
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UpdateAsync(List<LawyerSpecificService> lawyerSpecificService, Guid lawyerId)
        {
            // Lấy danh sách các service đã tồn tại của luật sư theo lawyerId
            try
            {
				var existingServices = _context.LawyerSpecificServices
			   .Where(x => x.LawyerId == lawyerId)
			   .ToList();

				// Xóa các service hiện có
				_context.LawyerSpecificServices.RemoveRange(existingServices);

				// Thêm các service mới vào context
				await _context.LawyerSpecificServices.AddRangeAsync(lawyerSpecificService);

				await _context.SaveChangesAsync();
                return true;
			}
            catch (Exception)
            {

                return false;
            }
        }
    }
}
