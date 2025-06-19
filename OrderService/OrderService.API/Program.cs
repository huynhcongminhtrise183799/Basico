using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Read;
using OrderService.Infrastructure.Write;
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
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DbContexts
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
            builder.Services.AddMassTransit(x =>
            {
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