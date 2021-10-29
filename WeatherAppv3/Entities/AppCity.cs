using System.Collections.Generic;

namespace WeatherAppv3.Entities
{
    public class AppCity
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public ICollection<Weather> WeatherData { get; set; }

    }
}