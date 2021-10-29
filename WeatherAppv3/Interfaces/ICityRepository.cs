using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAppv3.DTOs;
using WeatherAppv3.Entities;

namespace WeatherAppv3.Interfaces
{
    public interface ICityRepository
    {
        void Update(AppCity city);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppCity>> GetAppCitiesAsync();
        Task<AppCity> GetCityByIdAsync(int id);
        Task<AppCity> GetCityByCityNameAsync(string cityName);
        Task<IEnumerable<CityDataDto>> GetCityDatasAsync();
        Task<CityDataDto> GetCityDataAsync(string cityName);
        void DeleteCity(AppCity city);
    }
}