// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Collections.Generic;

// public class WeatherContext : DbContext
// {
//     public DbSet<Record> Records { get; set; }

//     public string DbPath { get; }

//     public WeatherContext()
//     {
       
//     }

//     // The following configures EF to create a Sqlite database file in the
//     // special "local" folder for your platform.
//     protected override void OnConfiguring(DbContextOptionsBuilder options)
//         => options.UseSqlServer($"Data Source={DbPath}");
// }

// public class Record
// {
//     public DateOnly Date { get; set; }
//     public int TemperatureC { get; set; }
//     public int TemperatureF { get; set; }
//     public string? Summary { get; set; }
// }
