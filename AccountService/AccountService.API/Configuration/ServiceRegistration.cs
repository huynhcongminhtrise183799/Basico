using AccountService.Domain.IRepositories;
using AccountService.Infrastructure.Read;
using AccountService.Infrastructure.Read.Repository;
using AccountService.Infrastructure.Write;
using AccountService.Infrastructure.Write.Repository;
using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

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

			// Token
			services.AddScoped<JwtTokenGenerator>();

			// DB
			services.AddDbContext<AccountDbContextWrite>(opt =>
				opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));

			services.AddDbContext<AccountDbContextRead>(opt =>
				opt.UseNpgsql(configuration.GetConnectionString("Postgres")));

			// Authentication
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
			})
			.AddCookie()
			.AddGoogle(options =>
			{
				options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
				options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
			});

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
