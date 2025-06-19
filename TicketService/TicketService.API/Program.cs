
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TicketService.Application.Consumer;
using TicketService.Application.Consumer.Ticket;
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
                x.AddConsumer<TicketCreatedConsumer>();
                x.AddConsumer<TicketRepliedConsumer>();
                x.AddConsumer<TicketDeletedConsumer>();
                x.AddConsumer<GetRequestAmountConsumer>();

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
            builder.Services.AddScoped<ITicketRepositoryWrite, TicketRepositoryWrite>();
            builder.Services.AddScoped<ITicketRepositoryRead, TicketRepositoryRead>();




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
