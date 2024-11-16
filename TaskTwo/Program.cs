
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using TaskTwo.Data;
using TaskTwo.Errors;

namespace TaskTwo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(
                options=>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections"))
                );
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddCors(
                options =>
                {
                    options.AddPolicy("AllowAll",
                         builder =>
                         {
                             builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                         }
                        );
                }
                );

            builder.Host.UseSerilog((context, Configuration) =>
            {
                Configuration.ReadFrom.Configuration(context.Configuration);
            });
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseExceptionHandler();
            app.UseCors("AllowAll");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
