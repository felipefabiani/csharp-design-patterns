using DecoratorDesignPattern.OpenWeatherMap.Models;

namespace DecoratorDesignPattern.OpenWeatherMap
{
    public interface IWeatherService
    {
        Task<CurrentWeather> GetCurrentWeather(string location);
        Task<LocationForecast> GetForecast(string location);
    }
}
