
using Microsoft.EntityFrameworkCore;
using TicketService.Infrastructure.Read;
using TicketService.Infrastructure.Write;

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
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TicketDbContextWrite>(opt =>
              opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

            builder.Services.AddDbContext<TicketDbContextRead>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

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
