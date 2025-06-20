using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ZadanieProbne2.DAL;
using ZadanieProbne2.Services;

namespace ZadanieProbne2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string? connectionString = builder.Configuration.GetConnectionString("db-mssql");
        
        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            options.JsonSerializerOptions.WriteIndented = true;
        });
        builder.Services.AddDbContext<CharacterDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(connectionString);
        });
        
        builder.Services.AddScoped<ICharacterService, CharacterService>();
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

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