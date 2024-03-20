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
        
        builder.Services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new() { Title = "PokemonManagingApp.Web", Version = "v1" });
            c.EnableAnnotations();
        });

        // Add other dependency injection to the program.
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddUseCases();

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
        app.Run();
    }
}
