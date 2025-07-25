﻿using AccountService.Application.DTOs.LawyerDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Queries.Lawyer
{
    public class GetAllLawyersQuery : IRequest<IEnumerable<LawyerDto>>
    { 
    }

}
