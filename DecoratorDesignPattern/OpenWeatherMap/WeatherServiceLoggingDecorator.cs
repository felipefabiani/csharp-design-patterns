using DecoratorDesignPattern.OpenWeatherMap.Models;
using MudBlazor;
using System.Diagnostics;

namespace DecoratorDesignPattern.OpenWeatherMap
{
    public class WeatherServiceLoggingDecorator : IWeatherService
    {
        private readonly IWeatherService innerWeatherService;
        private readonly ILogger<WeatherServiceLoggingDecorator> logger;

        public WeatherServiceLoggingDecorator(
            IWeatherService innerWeatherService, 
            ILogger<WeatherServiceLoggingDecorator> logger)
        {
            this.innerWeatherService = innerWeatherService ?? throw new ArgumentNullException(nameof(innerWeatherService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CurrentWeather> GetCurrentWeather(string location)
        {
            var sw = Stopwatch.StartNew();
            var response = await innerWeatherService.GetCurrentWeather(location);
            sw.Stop();
            logger.LogInformation($"Retrived weather location data for {location} - Elapsed ms: {sw.ElapsedMilliseconds}");
            return response;
        }

        public async Task<LocationForecast> GetForecast(string location)
        {
            var sw = Stopwatch.StartNew();
            var response = await innerWeatherService.GetForecast(location);
            sw.Stop();
            logger.LogInformation($"Retrived forecast location data for {location} - Elapsed ms: {sw.ElapsedMilliseconds}");
            return response;
        }
    }
}
