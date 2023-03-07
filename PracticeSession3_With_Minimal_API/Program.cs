using Microsoft.EntityFrameworkCore;
using PracticeSession3_With_Minimal_API.MinimalAPIs;
using PracticeSession3_With_Minimal_API.Models.Repository;
using PracticeSession3_With_Minimal_API.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//For EF Core 
var services = builder.Services;
services.AddDbContext<DataContext>(option =>
            option.UseSqlServer(builder.Configuration
            .GetConnectionString("StudentDB")));
services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//serilog
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

app.UseHttpsRedirection();

StudentAPIs.MapRoutes(app);

app.Run();
