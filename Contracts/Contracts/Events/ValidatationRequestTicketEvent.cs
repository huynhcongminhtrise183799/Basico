using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events
{
    public class ValidatationRequestTicket
    {
        public Guid AccountId { get; set; }
        public  bool IsValid { get; set; }

    }
}
