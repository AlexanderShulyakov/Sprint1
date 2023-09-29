using Microsoft.EntityFrameworkCore;

public class WeatherContext : DbContext
{
    public DbSet<Record> Records { get; set; }

    public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
    {

    }
}

public class Record
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF { get; set; }
    public string? Summary { get; set; }
}
