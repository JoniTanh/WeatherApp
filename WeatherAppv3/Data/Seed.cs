using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherAppv3.Entities;

namespace WeatherAppv3.Data
{
    public class Seed
    {
        public static async Task SeedCities(DataContext context)
        {
            if (await context.Cities.AnyAsync()) return;

            var cityData = await System.IO.File.ReadAllTextAsync("Data/CitySeedData.json");
            var cities = JsonSerializer.Deserialize<List<AppCity>>(cityData);
            foreach (var city in cities)
            {
                city.CityName = city.CityName.ToLower();

                await context.Cities.AddAsync(city);
            }

            await context.SaveChangesAsync();
        }
    }
}