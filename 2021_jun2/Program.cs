using Microsoft.EntityFrameworkCore;
using Models;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
services.AddDbContext<FakultetContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FakultetCS"));
});

services.AddCors(options => 
{
    options.AddPolicy("Cors", builder =>
    {
        builder.WithOrigins(new string[]
        {
            "https://localhost8080",
            "http://localhost8080",
            "http://127.0.0.1:8080",
            "https://127.0.0.1:8080",
            "https://google.com",
            "http://google.com",
        })
        .AllowAnyHeader()
        .AllowAnyMethod();

    });
});

// Add services to the container.
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

app.UseRouting();

app.UseCors("Cors");

app.UseAuthorization();

app.MapControllers();

app.Run();
