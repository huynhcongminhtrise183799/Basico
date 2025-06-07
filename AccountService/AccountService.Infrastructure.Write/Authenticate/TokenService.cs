using AccountService.Application.IService;
using AccountService.Domain.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Write.Authenticate
{
    public class TokenService : ITokenService
    {
        private readonly JwtOption _jwtOption;

        public TokenService(IOptions<JwtOption> jwtOption)
        {
            _jwtOption = jwtOption.Value ;
        }

        public string GenerateToken(Account account)
        {
           var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, account.AccountId.ToString()), 
                new Claim(ClaimTypes.Name, account.AccountFullName),
                new Claim(ClaimTypes.Email, account.AccountEmail),
                new Claim(ClaimTypes.Role, account.AccountRole)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOption.Issuer,
                audience: _jwtOption.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
