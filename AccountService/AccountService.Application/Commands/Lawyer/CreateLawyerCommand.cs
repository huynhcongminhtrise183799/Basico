using AccountService.Application.DTOs.LawyerDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Commands.Lawyer
{
    public class CreateLawyerCommand : IRequest<Guid>
    {
        public CreateLawyerDto Lawyer { get; set; }

    }
}
