using AccountService.API.OptionsSetup;
using AccountService.Application.Commands.AccountCommands;
using AccountService.Application.Consumers.AccountConsumers;
using AccountService.Application.Consumers.StaffConsumers;
using AccountService.Application.Handler.CommandHandler.AccountHandler;
using AccountService.Application.Handler.CommandHandler.ForgotPasswordCommandHandler;
using AccountService.Application.Handler.CommandHandler.StaffHandler;
using AccountService.Application.Handler.QueryHandler.AccountQueryHandler;
using AccountService.Application.Handler.QueryHandler.StaffQueryHandler;
using AccountService.Application.Consumers;
using AccountService.Application.Consumers.Lawyer;
using AccountService.Application.IService;
using AccountService.Application.Settings;
using AccountService.Domain.IRepositories;
using AccountService.Infrastructure.Read;
using AccountService.Infrastructure.Read.Repository;
using AccountService.Infrastructure.Write;
using AccountService.Infrastructure.Write.Authenticate;
using AccountService.Infrastructure.Write.Email;
using AccountService.Infrastructure.Write.Repository;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AccountService.Application.Message;
using AccountService.Infrastructure.Write.Message;
using AccountService.Application.Queries.Service;
using AccountService.Application.Queries.Lawyer;
using AccountService.Application.Consumers.LawyerDayOff;
using AccountService.Application.Consumers.Ticket;
using AccountService.Application.Handler.QueryHandler.Shift;
using AccountService.Application.Consumers.Form;



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
			services.AddScoped<ILawyerDayOffScheduleRepositoryRead, LawyerDayOffScheduleRepositoryRead>();
			services.AddScoped<ILawyerDayOffScheduleRepositoryWrite, LawyerDayOffScheduleRepositoryWrite>();
			services.AddScoped<ISpecificDayOffRepositoryRead, SpecificDayOffRepositoryRead>();
			services.AddScoped<ISpecificDayOffRepositoryWrite, SpecificDayOffRepositoryWrite>();
			services.AddScoped<IShiftRepositoryRead, ShiftRepositoryRead>();


			// Đăng ký MediatR
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(typeof(GoogleLoginCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(LoginUserCommandHandlerService).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(ProfileQueryHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(UpdateProfileCommand).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(CreateStaffCommandHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(UpdateStaffCommandHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(DeleteStaffCommandHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetAllStaffQueryHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetStaffByIdQueryHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetAllActiveStaffQueryHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(ResetPasswordCommandHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(VerifyOtpCommandHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(ForgotPasswordCommandHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetAllServiceByStatusQuery).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetLawyersByServiceIdQuery).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(RegisterAccountCommandHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetAllShiftHandlerService).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetAllAccountQueryHandlerService).Assembly);
			});
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegisterAccountCommandHandlerService>());


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
				x.AddConsumers(typeof(GoogleAccountCreatedConsumersService).Assembly);
				x.AddConsumer<AccountRegisteredEventConsumerService>();
				x.AddConsumer<UpdateProfileEventConsumerService>();
				x.AddConsumer<StaffCreatedEventConsumersService>();
				x.AddConsumer<StaffUpdatedEventConsumerService>();
				x.AddConsumer<StaffDeletedEventConsumerService>();
                x.AddConsumer<LawyerCreatedEventConsumerService>();
                x.AddConsumer<LawyerUpdatedEventConsumerService>();
                x.AddConsumer<LawyerDeletedEventConsumerService>();
				x.AddConsumer<CheckLawyerDayOffConsumerService>();
				x.AddConsumer<GetDetailBookingInformationConsumerService>();
				x.AddConsumer<UpdateAccountTicketRequestConsumerService>();
                x.AddConsumer<ValidatationRequestTicketConsumerService>();
				x.AddConsumer<DecreseTicketRequestConsumerService>();
				x.AddConsumer<BanAccountConsumerService>();
				x.AddConsumer<ActiveAccountConsumerService>();
				x.AddConsumer<DayOffCreatedConsumerService>();
				x.AddConsumer<DayOffJustifiedConsumerService>();
				x.AddConsumer<DayOffUpdatedConsumerService>();
				x.AddConsumer<DayOffDeletedConsumerService>();
				x.AddConsumer<BuyFormSuccessConsumerService>();

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
				cfg.EnableAnnotations();      
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
