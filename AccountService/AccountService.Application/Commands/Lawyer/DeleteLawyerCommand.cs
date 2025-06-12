using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Commands.Lawyer
{
    public class DeleteLawyerCommand : IRequest
    {
        public Guid AccountId { get; set; }
        public DeleteLawyerCommand(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
