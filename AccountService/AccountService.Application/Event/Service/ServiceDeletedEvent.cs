using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event.Service
{
    public class ServiceDeletedEvent
    {
        public Guid ServiceId { get; set; }
    }
}
