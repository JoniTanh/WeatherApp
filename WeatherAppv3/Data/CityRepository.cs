using System.Collections.Generic;
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
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CityRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppCity>> GetAppCitiesAsync()
        {
            return await _context.Cities
                .Include(w => w.WeatherData)
                .ToListAsync();
        }

        public async Task<AppCity> GetCityByIdAsync(int id)
        {
           return await _context.Cities.FindAsync(id);
        }

        public async Task<AppCity> GetCityByCityNameAsync(string cityName)
        {
            return await _context.Cities
                .Include(w => w.WeatherData)
                .SingleOrDefaultAsync(x => x.CityName == cityName);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppCity city)
        {
            _context.Entry(city).State = EntityState.Modified;
        }

        public async Task<IEnumerable<CityDataDto>> GetCityDatasAsync()
        {
            return await _context.Cities
                .ProjectTo<CityDataDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<CityDataDto> GetCityDataAsync(string cityName)
        {
            return await _context.Cities
                .Where(x => x.CityName == cityName)
                .ProjectTo<CityDataDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public void DeleteCity(AppCity city)
        {
            _context.Cities.Remove(city);
        }
    }
}