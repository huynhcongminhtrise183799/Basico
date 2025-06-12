using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event.Lawyer
{
    public class LawyerUpdatedEvent
    {
        public Guid AccountId { get; set; }
        public string AccountFullName { get; set; }
        public DateOnly AccountDob { get; set; }
        public int AccountGender { get; set; }
        public string? AccountPhone { get; set; }
        public string? AccountImage { get; set; }
        public string? AboutLawyer { get; set; }
    }
}
