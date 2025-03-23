using Company_API.Interfaces;
using Company_API.Models;
using Company_API.Repository;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
