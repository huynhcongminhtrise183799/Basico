
using AccountService.API.OptionsSetup;
using AccountService.Application.Commands;
using AccountService.Application.Consumers;
using AccountService.Application.Handler.CommandHandler;
using AccountService.Application.Handler.QueryHandler;
using AccountService.Application.IService;
using AccountService.Domain.IRepositories;
using AccountService.Infrastructure.Read;
using AccountService.Infrastructure.Read.Repository;
using AccountService.Infrastructure.Write;
using AccountService.Infrastructure.Write.Authenticate;
using AccountService.Infrastructure.Write.Repository;
using Example;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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


			// Đăng ký Repo
			services.AddScoped<IAccountRepositoryRead, AccountRepositoryRead>();
			services.AddScoped<IAccountRepositoryWrite, AccountRepositoryWrite>();

			// Đăng ký MediatR
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(typeof(GoogleLoginCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(LoginUserCommandHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(ProfileQueryHandler).Assembly);

            });
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegisterAccountCommandHandler>());


			// Behavior Options
			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

            //JWT Options
            builder.Services.ConfigureOptions<JwtOptionsSetup>();
            builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();


            // Token
            services.AddScoped<ITokenService, TokenService>();

			// DB
			services.AddDbContext<AccountDbContextWrite>(opt =>
				opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));

			services.AddDbContext<AccountDbContextRead>(opt =>
				opt.UseNpgsql(configuration.GetConnectionString("Postgres")));

			// MassTransit
			builder.Services.AddMassTransit(x =>
			{
				x.AddConsumers(typeof(GoogleAccountCreatedConsumers).Assembly);
				x.AddConsumer<AccountRegisteredEventConsumer>();
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

			builder.Services.AddSwaggerGen(cfg =>
			{
				cfg.AddSecurityDefinition(
					"Bearer",
					new OpenApiSecurityScheme
					{
						Name = "Authorization",
						Type = SecuritySchemeType.Http,
						Scheme = "Bearer",
						BearerFormat = "JWT",
						In = ParameterLocation.Header,
						Description =
							"Register a user, then authenticate using the respective endpoint, and add the token in the following input."
					}
				);

				cfg.AddSecurityRequirement(
					new OpenApiSecurityRequirement
					{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				},
				new string[] { }
			}
					}
				);
			});

		}
	}
}
