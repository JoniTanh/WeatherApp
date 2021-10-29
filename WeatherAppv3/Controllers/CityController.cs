using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherAppv3.Data;
using WeatherAppv3.DTOs;
using WeatherAppv3.Entities;
using WeatherAppv3.Interfaces;

namespace WeatherAppv3.Controllers
{
    public class CityController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ICityRepository _cityRepository;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IMapper _mapper;

        public CityController(DataContext context,
            ICityRepository cityRepository,
            IWeatherRepository weatherRepository,
            IMapper mapper)
        {
            _context = context;
            _cityRepository = cityRepository;
            _weatherRepository = weatherRepository;
            _mapper = mapper;
        }

        [HttpPost("add-city")]
        public async Task<ActionResult<CityDto>> AddCity(AddCityDto newCity)
        {
            if (await CityExists(newCity.CityName)) return BadRequest("City is already added");

            var city = new AppCity
            {
                CityName = newCity.CityName.ToLower()
            };

            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return new CityDto
            {
                CityName = city.CityName
            };
        }

        private async Task<bool> CityExists(string cityName)
        {
            return await _context.Cities.AnyAsync(x => x.CityName == cityName.ToLower());
        }

        [HttpDelete("delete-city/{id}")]
        public async Task<ActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);

            if (city == null) return BadRequest("Cannot delete the city that doesn't exists");

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("add-weather")]
        public async Task<ActionResult<WeatherDto>> AddWeather(AddWeatherDto newWeather)
        {
            var weather = new Weather
            {
                Date = newWeather.Date,
                TempC = newWeather.TempC,
                Rainfall = newWeather.Rainfall,
                WindSpeed = newWeather.WindSpeed,
                AppCityId = newWeather.AppCityId
            };

            if (weather.Rainfall < 0) return BadRequest("Value cannot be negative");
            if (weather.WindSpeed < 0) return BadRequest("Value cannot be negative");

            _context.WeatherData.Add(weather);
            await _context.SaveChangesAsync();

            return new WeatherDto
            {
                Date = newWeather.Date,
                TempC = newWeather.TempC,
                Rainfall = newWeather.Rainfall,
                WindSpeed = newWeather.WindSpeed
            };
        }

        [HttpDelete("delete-weather/{id}")]
        public async Task<ActionResult> DeleteWeather(int id)
        {
            var weather = await _context.WeatherData.FirstOrDefaultAsync(w => w.Id == id);

            if (weather == null) return BadRequest("Cannot delete the weather that doesn't exists");

            _context.WeatherData.Remove(weather);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateWeather(WeatherUpdateDto weatherUpdateDto)
        {
            var weather = new Weather
            {
                Id = weatherUpdateDto.Id,
                Date = weatherUpdateDto.Date,
                TempC = weatherUpdateDto.TempC,
                Rainfall = weatherUpdateDto.Rainfall,
                WindSpeed = weatherUpdateDto.WindSpeed,
                AppCityId = weatherUpdateDto.AppCityId
            };

            _context.WeatherData.Update(weather);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}