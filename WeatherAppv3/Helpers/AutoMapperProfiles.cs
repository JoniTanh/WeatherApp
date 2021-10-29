using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WeatherAppv3.DTOs;
using WeatherAppv3.Entities;

namespace WeatherAppv3.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppCity, CityDataDto>();
            CreateMap<Weather, WeatherDto>();
            CreateMap<WeatherUpdateDto, Weather>();
        }
    }
}