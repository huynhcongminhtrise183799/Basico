using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entity
{
    public enum ServiceStatus
	{
		Active,
		Inactive,
	}
	public class Service
    {
        public Guid ServiceId { get; set; }

        public string ServiceName { get; set; } 

        public string ServiceDescription { get; set; } 

        public string Status { get; set; }

		public virtual ICollection<LawyerSpecificService> LawyerSpecificServices { get; set; } = new List<LawyerSpecificService>();
    }
}
