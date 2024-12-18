using Microsoft.EntityFrameworkCore;

using TravelAgentWeb.Data;
using TravelAgentWeb.Utilities;

var builder = WebApplication.CreateBuilder(args);
ServiceRegistry.AddApplicationServices(builder.Services);

// Add DbContext with SQL Server
builder.Services.AddDbContext<TavelAgentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TravelAgentConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
PrepDB.PrepPopulation(app);

EndpointMapper.MapAllEndpoints(app);

app.Run();
