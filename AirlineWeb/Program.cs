using AirlineWeb.Data;
using AirlineWeb.Utilities;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();

// Add DbContext with SQL Server
builder.Services.AddDbContext<AirlineDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AirlineConnection")));

var app = builder.Build();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

EndpointMapper.MapAllEndpoints(app);
PrepDB.PrepPopulation(app);

await app.RunAsync();
