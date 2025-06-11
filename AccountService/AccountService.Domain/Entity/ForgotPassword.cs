using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entity
{
    public class ForgotPassword
    {
        public Guid ForgotPasswordId { get; set; }

        public string OTP { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid AccountId { get; set; }

        public Account Account { get; set; }
    }
}
