using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
    public interface IAccountRepositoryWrite
    {
        //Register
        Task AddAsync(Account account);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);


    }
}
