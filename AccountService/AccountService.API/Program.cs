
using AccountService.Infrastructure.Read;
using AccountService.Infrastructure.Write;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using AccountService.Application.Handler.CommandHandler;
using AccountService.Domain.IRepositories;
using AccountService.Infrastructure.Write.Repository;
using Microsoft.AspNetCore.Mvc;


namespace AccountService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegisterAccountCommandHandler>());
            builder.Services.AddScoped<IAccountRepositoryWrite, AccountRepositoryWrite>();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AccountDbContextWrite>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

            builder.Services.AddDbContext<AccountDbContextRead>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

            builder.Services.AddMassTransit(x =>
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
