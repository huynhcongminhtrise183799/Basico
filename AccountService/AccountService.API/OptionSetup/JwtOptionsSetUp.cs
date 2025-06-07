using AccountService.Infrastructure.Write.Authentication;
using Microsoft.Extensions.Options;

namespace AccountService.API.OptionSetup
{
	public class JwtOptionsSetUp : IConfigureOptions<JwtOption>
	{
		private const string SectionName = "Jwt";
		private readonly IConfiguration _configuration;

		public JwtOptionsSetUp(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Configure(JwtOption options)
		{
			_configuration.GetSection(SectionName).Bind(options);
		}
	}
}
