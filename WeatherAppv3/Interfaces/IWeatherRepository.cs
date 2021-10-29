using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAppv3.DTOs;
using WeatherAppv3.Entities;

namespace WeatherAppv3.Interfaces
{
    public interface IWeatherRepository
    {
        void AddWeather(Weather weather);
        void DeleteWeather(Weather weather);
        Task<CityDataDto> GetCityDataAsync(string cityName);
        void Update(Weather weather);
    }
}