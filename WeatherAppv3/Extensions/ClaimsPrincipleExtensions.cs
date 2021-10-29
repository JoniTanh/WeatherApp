using System.Security.Claims;

namespace WeatherAppv3.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetCityName(this ClaimsPrincipal city)
        {
            return city.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int GetCityId(this ClaimsPrincipal city)
        {
            return int.Parse(city.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}