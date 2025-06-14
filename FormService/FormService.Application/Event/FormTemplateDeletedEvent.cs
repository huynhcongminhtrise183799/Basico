using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Event
{
    public class FormTemplateDeletedEvent
    {
        public Guid Id { get; set; }
    }
}
