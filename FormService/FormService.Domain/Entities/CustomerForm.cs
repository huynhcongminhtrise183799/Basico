using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Domain.Entities
{
    public enum CustomerFormStatus
    {
        USED, NOTUSED
    }
    public class CustomerForm
    {
        public Guid CustomerFormId { get; set; }

        public Guid CustomerId { get; set; }

        public Guid FormTemplateId { get; set; }

        public string CustomerFormData { get; set; }

        public string Status { get; set; } // e.g., "SUBMITTED", "IN_PROGRESS", "COMPLETED"

        // Navigation property to the FormTemplate
        public virtual FormTemplate FormTemplate { get; set; }
    }
}
