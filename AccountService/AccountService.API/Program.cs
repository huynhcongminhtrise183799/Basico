using AccountService.API.Configuration;

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

			//if (app.Environment.IsDevelopment())
			//{
				app.UseSwagger();
				app.UseSwaggerUI();
			//}

			app.UseHttpsRedirection();

			// Bắt buộc: Authentication phải đặt trước Authorization
			app.UseAuthentication();
            /*app.UseCors(builder =>
builder.WithOrigins("http://localhost:3000")
       .AllowAnyHeader()
       .AllowAnyMethod());*/
            app.UseCors("AllowAll");
            app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
