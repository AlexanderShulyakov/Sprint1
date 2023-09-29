using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class WeatherContext : DbContext
{
    public DbSet<WeatherRecord> WeatherRecords { get; set; }

    public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
    {
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {

        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");

        base.ConfigureConventions(builder);
    }
}
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter()
        : base(dateOnly =>
                dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime))
    { }
}
