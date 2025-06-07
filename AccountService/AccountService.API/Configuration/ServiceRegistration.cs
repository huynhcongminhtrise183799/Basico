using AccountService.API.OptionSetup;
using AccountService.Application.Commands;
using AccountService.Application.Handler.QueryHandler;
using AccountService.Application.IService;
using AccountService.Domain.IRepositories;
using AccountService.Infrastructure.Read;
using AccountService.Infrastructure.Read.Repository;
using AccountService.Infrastructure.Write;
using AccountService.Infrastructure.Write.Authentication;
using AccountService.Infrastructure.Write.Repository;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace AccountService.API.Configuration
{
	public static class ServiceRegistration
	{
		public static void ConfigureServices(WebApplicationBuilder builder)
		{
			var services = builder.Services;
			var configuration = builder.Configuration;

			// Đăng ký Service
			services.AddScoped<IAccountRepositoryRead, AccountRepositoryRead>();
			services.AddScoped<IAccountRepositoryWrite, AccountRepositoryWrite>();
			// Đăng ký Repo

			// Đăng ký MediatR
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(typeof(GoogleLoginCommand).Assembly);
			});


			//JWT Options
			services.Configure<JwtOption>(configuration.GetSection("JwtOption"));
			builder.Services.ConfigureOptions<JwtOptionsSetUp>();
			builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

			// Token
			services.AddScoped<ITokenService, JwtTokenService>();

			// DB
			services.AddDbContext<AccountDbContextWrite>(opt =>
				opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));

			services.AddDbContext<AccountDbContextRead>(opt =>
				opt.UseNpgsql(configuration.GetConnectionString("Postgres")));

			// MassTransit
			services.AddMassTransit(x =>
			{
				x.UsingRabbitMq((context, cfg) =>
				{
					cfg.Host("localhost", "/", h =>
					{
						h.Username("guest");
						h.Password("guest");
					});

					cfg.ConfigureEndpoints(context);
				});
			});

			// Swagger and Controllers
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
		}
	}
}
