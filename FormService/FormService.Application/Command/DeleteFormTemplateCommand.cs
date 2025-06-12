using FormService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Command
{
    public record DeleteFormTemplateCommand(Guid id) : IRequest<bool>;

}
