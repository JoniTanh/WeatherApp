using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAppv3.DTOs
{
    public class AddCityDto
    {
        [Required]
        public string CityName { get; set; }
    }
}