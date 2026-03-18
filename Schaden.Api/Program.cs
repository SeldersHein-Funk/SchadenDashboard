
using Microsoft.Extensions.DependencyInjection;
using Schaden.Infrastructure;
using Schaden.Infrastructure.Database;

namespace Schaden.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<SchadenApplicationDbContext>();
            builder.Services.AddScoped<SchadenDbContext>((serviceFactory) => serviceFactory.GetRequiredService<SchadenApplicationDbContext>());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
