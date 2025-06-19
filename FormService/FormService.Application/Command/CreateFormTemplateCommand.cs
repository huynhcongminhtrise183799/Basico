using FormService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Command
{
    public record CreateFormTemplateCommand(Guid ServiceId, string FormTemplateName, string FormTemplateData, double Price) :IRequest<FormTemplateResponse>;
   
}
