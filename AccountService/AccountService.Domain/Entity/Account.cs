using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entity
{
    public enum Role
    {
        USER, LAWYER, STAFF, MANAGER
    }
    public enum Status
    {
        ACTIVE, INACTIVE
    }
    public class Account
    {
        public Guid AccountId { get; set; } //

        public string AccountUsername { get; set; }

        public string AccountPassword { get; set; }

        public string AccountEmail { get; set; } //

        public string AccountFullName { get; set; } //

        public DateOnly? AccountDob { get; set; }

        public int AccountGender { get; set; } //

        public string? AccountPhone { get; set; }

        public string? AccountImage { get; set; }

        public string AccountRole { get; set; }

        public string AccountStatus { get; set; }

        public int AccountTicketRequest { get; set; } // 
    }
}
