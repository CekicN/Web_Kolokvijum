using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.AspNetCore.Cors;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var services = builder.Services;
services.AddDbContext<AutomobilContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BazaCS"));
});
services.AddCors(options =>
            {
                options.AddPolicy("CORS", 
                
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
