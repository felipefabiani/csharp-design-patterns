using DecoratorDesignPattern.OpenWeatherMap.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DecoratorDesignPattern.OpenWeatherMap
{
    public class WeatherServiceCachingDecorator : IWeatherService
    {
        private readonly IWeatherService innerWeatherService;
        private readonly IMemoryCache memoryCache;

        public WeatherServiceCachingDecorator(
            IWeatherService innerWeatherService, 
            IMemoryCache memoryCache)
        {
            this.innerWeatherService = innerWeatherService ?? throw new ArgumentNullException(nameof(innerWeatherService));
            this.memoryCache = memoryCache;
        }

        public async Task<CurrentWeather> GetCurrentWeather(string location)
        {
            var cacheKey = $"WeatherConditions::{location.ToUpperInvariant()}";
            if (memoryCache.TryGetValue<CurrentWeather>(cacheKey, out var currentWeather))
            {
                return currentWeather;
            }

            var response = await innerWeatherService.GetCurrentWeather(location);
            memoryCache.Set(cacheKey, response, TimeSpan.FromMinutes(1));
            return response;
        }

        public async Task<LocationForecast> GetForecast(string location)
        {
            var cacheKey = $"ForecastConditions::{location.ToUpperInvariant()}";
            if (memoryCache.TryGetValue<LocationForecast>(cacheKey, out var locationForecast))
            {
                return locationForecast;
            }

            var response = await innerWeatherService.GetForecast(location);
            memoryCache.Set(cacheKey, response, TimeSpan.FromMinutes(1));
            return response;
        }
    }
}
