using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs
{
	public class PaginationQuery
	{
		public int Page { get; set; } = 1;
		public int? PageSize { get; set; }
	}

}
