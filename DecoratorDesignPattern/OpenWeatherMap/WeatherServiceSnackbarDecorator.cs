using DecoratorDesignPattern.OpenWeatherMap.Models;
using MudBlazor;
using System.Diagnostics;

namespace DecoratorDesignPattern.OpenWeatherMap
{
    public class WeatherServiceSnackbarDecorator : IWeatherService
    {
        private readonly IWeatherService innerWeatherService;
        private readonly ISnackbar snackbar;

        public WeatherServiceSnackbarDecorator(
            IWeatherService innerWeatherService,
            ISnackbar snackbar)
        {
            this.innerWeatherService = innerWeatherService ?? throw new ArgumentNullException(nameof(innerWeatherService));
            this.snackbar = snackbar ?? throw new ArgumentNullException(nameof(snackbar));
        }

        public ISnackbar Snackbar { get; }

        public async Task<CurrentWeather> GetCurrentWeather(string location)
        {
            var sw = Stopwatch.StartNew();
            var response = await innerWeatherService.GetCurrentWeather(location);
            sw.Stop();
            snackbar.Add(
                message: $"Retrived weather location data for {location} - Elapsed ms: {sw.ElapsedMilliseconds}",
                severity: Severity.Info);
            return response;
        }

        public async Task<LocationForecast> GetForecast(string location)
        {
            var sw = Stopwatch.StartNew();
            var response = await innerWeatherService.GetForecast(location);
            sw.Stop();
            snackbar.Add(
                message: $"Retrived forecast location data for {location} - Elapsed ms: {sw.ElapsedMilliseconds}",
                severity: Severity.Info);
            return response;
        }
    }
}
