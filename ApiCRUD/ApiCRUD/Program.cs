using Application.Interface;
using Application.Management;
using Core.Interface;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configuración de dependencias
//Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();


//Management
builder.Services.AddScoped<IManagementProduct, ManagementProduct>();

// Add services to the container.
IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
        optional: false,
        reloadOnChange: true)
    .Build();

string dbConnectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

//configurate logger with serilog and appsettings
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration) 
    .WriteTo.MySQL(connectionString: dbConnectionString)
    .CreateLogger();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

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
