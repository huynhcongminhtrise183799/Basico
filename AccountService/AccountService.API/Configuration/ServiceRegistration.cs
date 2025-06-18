
using AccountService.API.OptionsSetup;
using AccountService.Application.Commands.AccountCommands;
using AccountService.Application.Consumers.AccountConsumers;
using AccountService.Application.Consumers.StaffConsumers;
using AccountService.Application.Handler.CommandHandler.AccountHandler;
using AccountService.Application.Handler.CommandHandler.ForgotPasswordCommandHandler;
using AccountService.Application.Handler.CommandHandler.StaffHandler;
using AccountService.Application.Handler.QueryHandler.AccountQueryHandler;
using AccountService.Application.Handler.QueryHandler.StaffQueryHandler;
using AccountService.Application.Commands;
using AccountService.Application.Consumers;
using AccountService.Application.Consumers.Lawyer;
using AccountService.Application.Consumers.Service;
using AccountService.Application.Handler.CommandHandler;
using AccountService.Application.Handler.QueryHandler;
using AccountService.Application.IService;
using AccountService.Application.Settings;
using AccountService.Domain.IRepositories;
using AccountService.Infrastructure.Read;
using AccountService.Infrastructure.Read.Repository;
using AccountService.Infrastructure.Write;
using AccountService.Infrastructure.Write.Authenticate;
using AccountService.Infrastructure.Write.Email;
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
using AccountService.Application.Message;
using AccountService.Infrastructure.Write.Message;
using AccountService.Application.Queries.Service;
using AccountService.Application.Queries.Lawyer;
using AccountService.Application.Consumers.LawyerDayOff;


namespace AccountService.API.Configuration
{
	public static class ServiceRegistration
	{
		public static void ConfigureServices(WebApplicationBuilder builder)
		{
			var services = builder.Services;
			var configuration = builder.Configuration;

			// Đăng ký Service
			services.AddScoped<IEmailService, EmailService>();


			// Đăng ký Repo
			services.AddScoped<IAccountRepositoryRead, AccountRepositoryRead>();
			services.AddScoped<IAccountRepositoryWrite, AccountRepositoryWrite>();
			services.AddScoped<ILawyerSpecificServiceRepositoryRead, LawyerSpecificServiceRepositoryRead>();
            services.AddScoped<ILawyerSpecificServiceRepositoryWrite, LawyerSpecificServiceRepositoryWrite>();
			services.AddScoped<IServiceRepositoryRead, ServiceRepositoryRead>();
			services.AddScoped<IShiftRepositoryRead, ShiftRepositoryRead>();
			builder.Services.AddScoped<IEventPublisher, MassTransitEventPublisher>();


            // Đăng ký MediatR
            services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(typeof(GoogleLoginCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(LoginUserCommandHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(ProfileQueryHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(UpdateProfileCommand).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(CreateStaffCommandHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(UpdateStaffCommandHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(DeleteStaffCommandHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetAllStaffQueryHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetStaffByIdQueryHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetAllActiveStaffQueryHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(ResetPasswordCommandHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(VerifyOtpCommandHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(ForgotPasswordCommandHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetAllServiceByStatusQuery).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetLawyersByServiceIdQuery).Assembly);
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

			// Bộ nhớ tạm thời
			builder.Services.AddMemoryCache();

			// Token
			services.AddScoped<ITokenService, TokenService>();

			// DB
			services.AddDbContext<AccountDbContextWrite>(opt =>
				opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));

			services.AddDbContext<AccountDbContextRead>(opt =>
				opt.UseNpgsql(configuration.GetConnectionString("Postgres")));

			// Cấu hình Email

			services.Configure<EmailSettings>
				(builder.Configuration.GetSection("EmailSettings"));


			// MassTransit
			builder.Services.AddMassTransit(x =>
			{
				x.AddConsumers(typeof(GoogleAccountCreatedConsumers).Assembly);
				x.AddConsumer<AccountRegisteredEventConsumer>();
				x.AddConsumer<UpdateProfileEventConsumer>();
				x.AddConsumer<StaffCreatedEventConsumers>();
				x.AddConsumer<StaffUpdatedEventConsumer>();
				x.AddConsumer<StaffDeletedEventConsumer>();
                x.AddConsumer<LawyerCreatedEventConsumer>();
                x.AddConsumer<LawyerUpdatedEventConsumer>();
                x.AddConsumer<LawyerDeletedEventConsumer>();
				x.AddConsumer<CheckLawyerDayOffConsumer>();
				x.AddConsumer<GetLawyerNameConsumer>();
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
