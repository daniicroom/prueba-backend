using Application.Interface;
using Application.Management;
using Core.Interface.Repositories;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

// Add services to the container.
IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
        optional: false,
        reloadOnChange: true)
    .Build();

string dbConnectionString = configuration.GetConnectionString("DefaultConnection");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

//configurate logger with serilog and appsettings
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.MySQL(connectionString: dbConnectionString)
    .CreateLogger();

Log.Information($"Api Starting in {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog();
});

// Configuración de dependencias
//Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();


//Management
builder.Services.AddScoped<IManagementProduct, ManagementProduct>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
//builder.Services.AddAuth(jwtSettings);

var app = builder.Build();
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
