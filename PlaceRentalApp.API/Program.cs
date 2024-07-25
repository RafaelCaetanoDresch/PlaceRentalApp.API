using Microsoft.EntityFrameworkCore;
using PlaceRentalApp.API.Middlewares;
using PlaceRentalApp.API.Models;
using PlaceRentalApp.API.Persistence;

namespace PlaceRentalApp.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("PlaceRentalCs");
        builder.Services.AddDbContext<PlaceRentalDbContext>(
            o => o.UseSqlServer(connectionString));


        var min = builder.Configuration.GetValue<int>("PlacesConfig:MinDescription");
        var max = builder.Configuration.GetValue<int>("PlacesConfig:MaxDescription");

        builder.Services.Configure<PlacesConfiguration>(
            builder.Configuration.GetSection("PlacesConfig"));


        builder.Services.AddExceptionHandler<ApiExcepationHandler>();
        builder.Services.AddProblemDetails();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseExceptionHandler();

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
