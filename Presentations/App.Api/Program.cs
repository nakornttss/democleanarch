using App.AppCore.Applications;
using App.AppCore.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: true, true)
                            .AddJsonFile($"appsettings.{environment}.json", true, true)
                            .AddEnvironmentVariables()
                            .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Select storage base on configuration
if (config["Execution:Storage"] == "NoStorage")
{
    builder.Services.AddScoped<IStorage, App.NoStorage.DoNotKeep>();
}
else if (config["Execution:Storage"] == "File")
{
    builder.Services.AddScoped<IStorage, App.File.KeepLogInFile>();
}
else if (config["Execution:Storage"] == "Database")
{
    builder.Services.AddScoped<IStorage, App.Database.KeepLogInDb>();
}

// Select process base on configuration
if (config["Execution:AppCore"] == "ProcessMock")
{
    builder.Services.AddScoped<IProcessInputOutout, ProcessMock>();
}
else if (config["Execution:AppCore"] == "ProcessMinus")
{
    builder.Services.AddScoped<IProcessInputOutout, ProcessMinus>();
}
else if (config["Execution:AppCore"] == "ProcessPlus")
{
    builder.Services.AddScoped<IProcessInputOutout, ProcessPlus>();
}



builder.Services.AddDbContext<App.Database.AppContext>(c =>
    c.UseSqlServer(@"Server=.\\SQLEXPRESS;Integrated Security=true;Initial Catalog=cleandb;TrustServerCertificate=True")
);

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
