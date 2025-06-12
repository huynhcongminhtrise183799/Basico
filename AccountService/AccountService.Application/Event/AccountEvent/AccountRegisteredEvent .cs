using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event.AccountEvent
{
    public class AccountRegisteredEvent
    {
        public Guid AccountId { get; set; }
        public string AccountUsername { get; set; }
        public string AccountPassword { get; set; }
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string AccountEmail { get; set; }
        public string AccountFullName { get; set; }
        public int AccountGender { get; set; }
        public string AccountRole { get; set; }
        public string AccountStatus { get; set; }
        public int AccountTicketRequest { get; set; }

    }
}
