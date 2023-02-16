using App.AppCore.Applications;
using App.AppCore.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IStorage, App.Database.KeepLogInDb>();
builder.Services.AddScoped<IProcessInputOutout, ProcessPlus>();

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
