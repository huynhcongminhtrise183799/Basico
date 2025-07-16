using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events
{
    public class SendEmailBookingEvent
    {
        public Guid? AccountId { get; set; }
    }
}
