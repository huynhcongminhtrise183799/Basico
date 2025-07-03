using AccountService.API.Configuration;

using AccountService.API.OptionsSetup;
using AccountService.Application.Handler.CommandHandler;
using AccountService.Application.Handler.QueryHandler;
using AccountService.Application.IService;
using AccountService.Domain.IRepositories;
using AccountService.Infrastructure.Read;
using AccountService.Infrastructure.Read.Repository;
using AccountService.Infrastructure.Write;
using AccountService.Infrastructure.Write.Authenticate;
using AccountService.Infrastructure.Write.Repository;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AccountService.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
			// Gọi service registration
			ServiceRegistration.ConfigureServices(builder);
  
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			//app.UseHttpsRedirection();
            app.UseCors("AllowAll");

			// Bắt buộc: Authentication phải đặt trước Authorization
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
