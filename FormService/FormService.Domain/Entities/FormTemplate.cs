using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Domain.Entities
{
    public enum Status
    {
        ACTIVE, INACTIVE
    }
    public class FormTemplate
    {
        public Guid FormTemplateId { get; set; }

        public Guid ServiceId { get; set; }

        public string FormTemplateName { get; set; }

        public string FormTemplateData { get; set; }

        public double Price { get; set; }

		public string Status { get; set; }
    }
}
