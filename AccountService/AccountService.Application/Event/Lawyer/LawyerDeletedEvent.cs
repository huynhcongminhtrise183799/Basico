using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event.Lawyer
{
    public class LawyerDeletedEvent
    {
        public Guid AccountId { get; set; }
    }
}
