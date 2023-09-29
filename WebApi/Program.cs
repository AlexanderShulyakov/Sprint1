using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WeatherContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


var app = builder.Build();
//Ensure DB creation and migrations applying
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<WeatherContext>())
    context.Database.Migrate();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.MapProductEndpoints();
app.Run();
