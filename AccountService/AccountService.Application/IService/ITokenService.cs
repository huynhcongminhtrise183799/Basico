using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.IService
{
    public interface ITokenService
    {
        string GenerateToken(Account account);

    }
}
