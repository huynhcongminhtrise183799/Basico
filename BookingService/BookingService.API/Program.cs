
using BookingService.Application.Consumer;
using BookingService.Application.Handler.CommandHandler;
using BookingService.Application.Handler.QueryHandler;
using BookingService.Application.Message;
using BookingService.Domain.IRepository;
using BookingService.Infrastructure.Read;
using BookingService.Infrastructure.Read.Repository;
using BookingService.Infrastructure.Write;
using BookingService.Infrastructure.Write.BackgroundServices;
using BookingService.Infrastructure.Write.Message;
using BookingService.Infrastructure.Write.Repository;
using Contracts;
using Contracts.Events;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookingService.API
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(cfg =>
			{
				cfg.EnableAnnotations();
			});
			builder.Services.AddDbContext<BookingDbContextWrite>(opt =>
				opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

			builder.Services.AddDbContext<BookingDbContextRead>(opt =>
				opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

			// Register other services, repositories, and MediatR
			builder.Services.AddScoped<IBookingRepositoryWrite, BookingRepositoryWrite>();
			builder.Services.AddScoped<IBookingRepositoryRead, BookingRepositoryRead>();
			builder.Services.AddScoped<ISlotRepositoryRead, SlotRepositoryRead>();
			builder.Services.AddScoped<IBookingSlotRepositoryRead, BookingSlotRepositoryRead>();
			builder.Services.AddScoped<IBookingSlotsRepositoryWrite, BookingSlotRepositoryWrite>();
			builder.Services.AddScoped<IEventPublisher, MassTransitEventPublisher>();
			builder.Services.AddScoped<IFeedbackRepositoryRead , FeedbackRepositoryRead>();
			builder.Services.AddScoped<IFeedbackRepositoryWrite , FeedbackRepositoryWrite>();
			builder.Services.AddHostedService<BookingStatusBackgroundService>();


			builder.Services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(typeof(CreateBookingHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetFreeSlotsForLawyerHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(UpdateBookingHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(CancelBookingHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetFreeSlotsForUpdateHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(CreateFeedbackHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(UpdateFeedbackHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetAllFeedbackHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetDetailFeedbackHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(GetFeedbackByBookingIdHandler).Assembly);

            });

			builder.Services.AddMassTransit(x =>
			{
				x.AddConsumer<CreateBookingConsumer>();
				x.AddConsumer<CancelBookingConsumer>();
				x.AddConsumer<UpdateBookingConsumer>();
				x.AddConsumer<CheckInBookingConsumer>();
				x.AddConsumer<PaymentSuccessConsumer>();
				x.AddConsumer<CheckOutBookingConsumer>();
				x.AddConsumer<FeedbackCreatedConsumer>();
				x.AddConsumer<FeedbackUpdatedConsumer>();
				x.AddConsumer<BookingOverTimeStatusChangedConsumer>();
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
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

            app.UseCors(builder =>
         builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
