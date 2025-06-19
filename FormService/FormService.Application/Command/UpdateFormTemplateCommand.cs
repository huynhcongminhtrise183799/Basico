using FormService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Command
{
    public record UpdateFormTemplateCommand(Guid id, string name, string data, Guid serviceId, string status, double Price) : IRequest<FormTemplateResponse>;

}
