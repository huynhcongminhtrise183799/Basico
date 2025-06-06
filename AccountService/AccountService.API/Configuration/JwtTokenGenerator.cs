using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountService.API.Configuration
{
	public class JwtTokenGenerator
	{
		private readonly IConfiguration _configuration;

		public JwtTokenGenerator(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerateToken(string email, string name)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.Email, email),
				new Claim(ClaimTypes.Name, name)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpiresInMinutes"])),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
