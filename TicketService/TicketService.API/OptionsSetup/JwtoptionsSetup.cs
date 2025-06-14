using Microsoft.Extensions.Options;
using TicketService.Infrastructure.Write.Authenticate;

namespace TicketService.API.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOption>
    {
        private const string SectionName = "Jwt";
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(JwtOption options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
