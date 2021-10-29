using Microsoft.EntityFrameworkCore;
using WeatherAppv3.Entities;

namespace WeatherAppv3.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppCity> Cities { get; set; }
        public DbSet<Weather> WeatherData { get; set; }
    }
}