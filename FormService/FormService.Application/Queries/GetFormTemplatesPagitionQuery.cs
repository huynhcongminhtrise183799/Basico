using FormService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Queries
{
    public record GetFormTemplatesPagitionQuery(int page) : IRequest<List<FormTemplateResponse>>;
}
