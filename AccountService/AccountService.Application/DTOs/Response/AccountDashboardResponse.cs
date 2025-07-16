using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Response
{
	public class AccountDashboardResponse
	{
		public int AllAccounts { get; set; }

		public int AllUserAcccounts { get; set; }

		public int AllLawyerAccounts { get; set; }

		public int AllStaffAccounts { get; set; }

		public int AllInactiveAccounts { get; set; }

		public int AllInactiveUserAccounts { get; set; }

		public int AllInactiveLawyerAccounts { get; set; }

		public int AllInactiveStaffAccounts { get; set; }
	}
}
