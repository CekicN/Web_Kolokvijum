using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FotoContext>(option => 
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("BazaCS"));
});
builder.Services.AddCors(options =>
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
