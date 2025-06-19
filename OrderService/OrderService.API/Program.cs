using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Command;
using OrderService.Application.Consumer;
using OrderService.Application.Event;
using OrderService.Application.Handler.CommandHandler;
using OrderService.Application.IService;
using OrderService.Application.Service;
using OrderService.Domain.IRepositories;
using OrderService.Infrastructure.Read;
using OrderService.Infrastructure.Read.Repositories;
using OrderService.Infrastructure.Write;
using OrderService.Infrastructure.Write.Repositories;
using OrderService.Infrastructure.Write.Repositories;
using MediatR;
using OrderService.Application.Handler.CommandHandler;
using OrderService.Infrastructure.Read.Repositories;
using OrderService.Application.Consumer;

namespace OrderService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddScoped<IPaymentService, PaymentService>();
			// Add Repo
			builder.Services.AddScoped<IOrderRepositoryWrite, OrderRepositoryWrite>();
			builder.Services.AddScoped<IOrderRepositoryRead, OrderRepositoryRead>();
			builder.Services.AddScoped<IPaymentRepositoryRead, PaymentRepositoryRead >();
			builder.Services.AddScoped<IPaymentRepositoryWrite, PaymentRepositoryWrite >();

			// Đăng ký MediatR
			builder.Services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(typeof(CreatePaymentCommand).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(CreatePaymentCommandHandler).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(CreatePaymentEvent).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(CreatePaymentConsumer).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly);
			});

			// MassTransit
			builder.Services.AddMassTransit(x =>
			{
				x.AddConsumers(typeof(CreatePaymentConsumer).Assembly);
				x.AddConsumers(typeof(CreateOrderConsumer).Assembly);
				x.AddConsumer<OrderCreatedEventConsumer>();

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

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<OrderDbContextWrite>(opt =>
				opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

			builder.Services.AddDbContext<OrderDbContextRead>(opt =>
				opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));


            //// Typed HttpClient cho TicketService
            //builder.Services.AddHttpClient<ITicketPackageService, TicketPackageHttpService>(c =>
            //{
            //    c.BaseAddress = new Uri(builder.Configuration["TicketService:BaseUrl"]);
            //});

            // MassTransit (RabbitMQ)
            
            builder.Services.AddScoped<IOrderRepositoryRead, OrderRepositoryRead>();

            // MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateOrderCommandHandler>());

            // Register Repository (CQRS Write)
            builder.Services.AddScoped<IOrderRepositoryWrite, OrderRepositoryWrite>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}