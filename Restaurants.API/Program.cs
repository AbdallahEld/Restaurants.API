using Restaurants.API.Controllers;
using Restaurants.API.Middlewares;
using Restaurants.Application;
using Restaurants.Domain;
using Restaurants.Infrastructure;
using Serilog;
using Serilog.Events;

namespace Restaurants.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<TimeLoggingMiddleware>();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration);
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
            await seeder.Seed();
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<TimeLoggingMiddleware>();

            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapIdentityApi<User>();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
