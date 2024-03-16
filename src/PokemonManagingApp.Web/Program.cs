using PokemonManagingApp.Infrastructure;
using PokemonManagingApp.UseCases;

internal partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        

        builder.Services.AddUseCases();
        builder.Services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new() { Title = "PokemonManagingApp.Web", Version = "v1" });
            c.EnableAnnotations();
        });

        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.MapControllers();

        //         var summaries = new[]
        //         {
        //         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        // };

        //         app.MapGet("/weatherforecast", () =>
        //         {
        //             var forecast = Enumerable.Range(1, 5).Select(index =>
        //             new WeatherForecast
        //             (
        //                     DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //                     Random.Shared.Next(-20, 55),
        //                     summaries[Random.Shared.Next(summaries.Length)]
        //             ))
        //             .ToArray();
        //             return forecast;
        //         })
        //         .WithName("GetWeatherForecast")
        //         .WithOpenApi();

        app.Run();
    }
}

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
