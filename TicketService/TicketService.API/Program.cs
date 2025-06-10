
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketService.Application.Consumer;
using TicketService.Application.Handler.CommandHandler;
using TicketService.Domain.IRepositories;
using TicketService.Infrastructure.Read;
using TicketService.Infrastructure.Read.Repository;
using TicketService.Infrastructure.Write;
using TicketService.Infrastructure.Write.Repository;

namespace TicketService.API
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateTicketPackageCommandHandler>());

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<TicketPackageCreatedConsumer>();
                x.AddConsumer<TicketPackageUpdatedConsumer>();
                x.AddConsumer<TicketPackageDeletedConsumer>();
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


            builder.Services.AddDbContext<TicketDbContextWrite>(opt =>
              opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

            builder.Services.AddDbContext<TicketDbContextRead>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

            builder.Services.AddScoped<ITicketPackageRepositoryRead, TicketPackageRepositoryRead>();
            builder.Services.AddScoped<ITicketPackageRepositoryWrite, TicketPackageRepositoryWrite>();

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
