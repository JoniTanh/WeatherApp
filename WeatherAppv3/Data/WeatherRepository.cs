using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WeatherAppv3.DTOs;
using WeatherAppv3.Entities;
using WeatherAppv3.Interfaces;

namespace WeatherAppv3.Data
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public WeatherRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddWeather(Weather weather)
        {
            _context.WeatherData.Add(weather);
        }

        public void DeleteWeather(Weather weather)
        {
            _context.WeatherData.Remove(weather);
        }

        public async Task<CityDataDto> GetCityDataAsync(string cityName)
        {
            return await _context.Cities
                .Where(x => x.CityName == cityName)
                .ProjectTo<CityDataDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public void Update(Weather weather)
        {
            _context.Entry(weather).State = EntityState.Modified;
        }
    }
}