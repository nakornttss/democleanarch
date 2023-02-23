using App.Api;
using App.AppCore.Applications;
using App.AppCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


// In case you want to add more configuration such as read from Azure Vault, you can add like this

//builder.Configuration
//    .AddEnvironmentVariables(); // azure is here

// In case you do not want to use default configuration in builder.Configuration such as you need your order of configuration reading
// you can make your customization here. 
// If you want to do so, change builder.Configuration to config for all code in this file

//var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
//var config = new ConfigurationBuilder()
//                            .AddJsonFile("appsettings.json", optional: true, true)
//                            .AddJsonFile($"appsettings.{environment}.json", true, true)
//                            .AddEnvironmentVariables()
//                            .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure options
var executionConfig = new ExecutionOptions();
builder.Configuration.GetSection(ExecutionOptions.TagName).Bind(executionConfig);

builder.Services.Configure<ExecutionOptions>(builder.Configuration.GetSection(ExecutionOptions.TagName));

// Select storage base on configuration
if (executionConfig.Storage == AppStorage.NoStorage)
{
    builder.Services.AddScoped<IStorage, App.NoStorage.DoNotKeep>();
}
else if (executionConfig.Storage == AppStorage.File)
{
    builder.Services.AddScoped<IStorage, App.File.KeepLogInFile>();
}
else if (executionConfig.Storage == AppStorage.Database)
{
    builder.Services.AddScoped<IStorage, App.Database.KeepLogInDb>();
}

// Select process base on configuration
if (executionConfig.AppCore == AppCore.ProcessMock)
{
    builder.Services.AddScoped<IProcessInputOutout, ProcessMock>();
}
else if (executionConfig.AppCore == AppCore.ProcessMinus)
{
    builder.Services.AddScoped<IProcessInputOutout, ProcessMinus>();
}
else if (executionConfig.AppCore == AppCore.ProcessPlus)
{
    builder.Services.AddScoped<IProcessInputOutout, ProcessPlus>();
}

builder.Services.AddDbContext<App.Database.AppContext>(c =>
    c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
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

