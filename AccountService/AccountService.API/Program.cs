using AccountService.API.Configuration;

using AccountService.Infrastructure.Read;
using AccountService.Infrastructure.Write;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AccountService.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Gọi service registration
			ServiceRegistration.ConfigureServices(builder);

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			// Bắt buộc: Authentication phải đặt trước Authorization
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
