using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountService.Application.DTOs.Response;
using MediatR;

namespace AccountService.Application.Commands
{
	public record GoogleLoginCommand(string Email, string FullName) : IRequest<GoogleLoginResponse>;
}
