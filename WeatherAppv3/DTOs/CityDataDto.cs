using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAppv3.DTOs
{
    public class CityDataDto
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public ICollection<WeatherDto> WeatherData { get; set; }
    }
}