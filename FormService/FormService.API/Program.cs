
using FormService.Application.Consumer;
using FormService.Application.Handler.CommandHandler;
using FormService.Domain.IRepositories;
using FormService.Infrastructure.Read;
using FormService.Infrastructure.Read.Repository;
using FormService.Infrastructure.Write;
using FormService.Infrastructure.Write.Repository;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace FormService.API
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
            builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<CreateFormTemplateCommandHandlerService>()
                );

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<FormTemplateCreatedConsumerService>();
                x.AddConsumer<FormTemplateUpdatedConsumerService>();
                x.AddConsumer<FormTemplateDeletedConsumerService>();
                x.AddConsumer<CreateCustomerFormDataConsumerService>();
                x.AddConsumer<CustomerFormUpdatedConsumerService>();
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


            builder.Services.AddDbContext<FormDbContextWrite>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

            builder.Services.AddDbContext<FormDbContextRead>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

            // Register repositories
            builder.Services.AddScoped<IFormTemplateRepositoryWrite, FormTemplateRepositoryWrite>();
            builder.Services.AddScoped<IFormTemplateRepositoryRead, FormTemplateRepositoryRead>();
            builder.Services.AddScoped<IFormDataRepositoryWrite, FormDataRepositoryWrite>();
            builder.Services.AddScoped<IFormDataRepositoryRead, FormDataRepositoryRead>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.UseAuthorization();
            app.UseCors(builder =>
builder.AllowAnyOrigin()
 .AllowAnyHeader()
 .AllowAnyMethod());


            app.MapControllers();

            app.Run();
        }
    }
}
