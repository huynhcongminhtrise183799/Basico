using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TicketService.Infrastructure.Write.Authenticate;

namespace TicketService.API.OptionsSetup
{
    public class JwtBearerOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly JwtOption _jwtOption;
        public JwtBearerOptionsSetup(IOptions<JwtOption> jwtOption)
        {
            _jwtOption = jwtOption.Value;
        }
        public void PostConfigure(string? name, JwtBearerOptions options)
        {
            options.TokenValidationParameters.ValidIssuer = _jwtOption.Issuer;
            options.TokenValidationParameters.ValidAudience = _jwtOption.Audience;
            options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_jwtOption.SecretKey));

        }
    }
}
