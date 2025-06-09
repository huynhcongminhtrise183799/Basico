using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entity
{
    public class Service
    {
        public Guid ServiceId { get; set; } // Unique identifier for the service

        public string ServiceName { get; set; } // Name of the service

        public string ServiceDescription { get; set; } // Description of the service

        public virtual ICollection<LawyerSpecificService> LawyerSpecificServices { get; set; } = new List<LawyerSpecificService>();
    }
}
