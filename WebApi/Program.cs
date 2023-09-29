using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string connectionString = string.Empty;
if (!builder.Environment.IsDevelopment())
{
    SecretClientOptions options = new SecretClientOptions()
    {
        Retry =
        {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
         }
    };
    var client = new SecretClient(new Uri(builder.Configuration.GetValue<string>("KeyVault:VaultUri")), new DefaultAzureCredential(), options);
    KeyVaultSecret secret = client.GetSecret(builder.Configuration.GetValue<string>("KeyVault:SecretName"));

    connectionString = secret.Value;
}
else
{
    connectionString = builder.Configuration.GetConnectionString("Default");

}
builder.Services.AddDbContext<WeatherContext>(options => options.UseSqlServer(connectionString));
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
