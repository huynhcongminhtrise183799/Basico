using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event.Service
{
    public class ServiceCreatedEvent
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
    }
}
