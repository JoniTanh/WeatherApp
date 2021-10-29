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
    public class CitiesController : BaseApiController
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public CitiesController(ICityRepository cityRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDataDto>>> GetCities()
        {
            var cities = await _cityRepository.GetCityDatasAsync();

            return Ok(cities);
        }

        [HttpGet("{cityName}")]
        public async Task<ActionResult<CityDataDto>> GetCity(string cityName)
        {
            return await _cityRepository.GetCityDataAsync(cityName);
        }
    }
}