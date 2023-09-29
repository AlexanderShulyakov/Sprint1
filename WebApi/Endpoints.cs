using System.Linq;

public static class ProductEndpointsExt
{
    public static string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
    public static void MapProductEndpoints(this WebApplication app)
    {

        app.MapGet("/weatherforecast", () =>
        {
            //Generate weather forecast
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]
                ))
                .ToArray();

            //Save to DB
            using (var scope = app.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<WeatherContext>())
            {
                var records = forecast.ToList().Select(f =>
                new WeatherRecord() { Date = f.Date, Summary = f.Summary, TemperatureC = f.TemperatureC, TemperatureF = f.TemperatureF });
                context.AddRange(records);
                context.SaveChanges();
            }
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    }
}
