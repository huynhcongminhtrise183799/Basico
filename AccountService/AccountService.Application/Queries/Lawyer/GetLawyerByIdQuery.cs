using AccountService.Application.DTOs.LawyerDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Queries.Lawyer
{
    public class GetLawyerByIdQuery : IRequest<LawyerDto>
    {
        public Guid AccountId { get; set; }
        public GetLawyerByIdQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
