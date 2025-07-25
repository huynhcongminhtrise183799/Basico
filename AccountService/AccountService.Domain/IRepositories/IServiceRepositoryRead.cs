﻿using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
    public interface IServiceRepositoryRead
    {

		Task<List<Service>> GetServicesByStatusAsync(string status);

		Task<string?> GetServiceNameByServiceId(Guid id);
	}
}
