using DecoratorDesignPattern.OpenWeatherMap.Models;
using Newtonsoft.Json;
using System.Net;

namespace DecoratorDesignPattern.OpenWeatherMap
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherClient _weatherClient;

        public WeatherService(WeatherClient weatherClient)
        {
            _weatherClient = weatherClient;
        }

        public async Task<CurrentWeather> GetCurrentWeather(string location)
        {
            var response = await _weatherClient.GetCurrentWeather(location);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => await DeserializeObject(),
                HttpStatusCode.NotFound => CurrentWeather.NotFound(location),
                HttpStatusCode.BadRequest => CurrentWeather.BadRequest(),
                _ => CurrentWeather.Error(),
            };

            async Task<CurrentWeather> DeserializeObject()
            {
                var json = await response!.Content.ReadAsStringAsync();
                var currentConditions = JsonConvert.DeserializeObject<CurrentConditionsResponse>(json)!;
                return MapCurrentConditionsResponse(currentConditions);

                static CurrentWeather MapCurrentConditionsResponse(CurrentConditionsResponse openWeatherApiResponse)
                {
                    var currentConditions = new CurrentWeather()
                    {
                        Success = true,
                        ErrorMessage = string.Empty,
                        Location = new CurrentWeather.LocationData()
                        {
                            Name = openWeatherApiResponse.Name,
                            Latitude = openWeatherApiResponse.Coordintates.Latitude,
                            Longitude = openWeatherApiResponse.Coordintates.Longitude
                        },
                        ObservationTime = DateTimeOffset.FromUnixTimeSeconds(openWeatherApiResponse.ObservationTime + openWeatherApiResponse.TimezoneOffset).DateTime,
                        ObservationTimeUtc = DateTimeOffset.FromUnixTimeSeconds(openWeatherApiResponse.ObservationTime).DateTime,
                        CurrentConditions = new CurrentWeather.WeatherData()
                        {
                            Conditions = openWeatherApiResponse.ObservedConditions.FirstOrDefault()?.Conditions,
                            ConditionsDescription = openWeatherApiResponse.ObservedConditions.FirstOrDefault()?.ConditionsDetail,
                            Visibility = openWeatherApiResponse.Visibility / 1609.0,  // Visibility always comes back in meters, even when imperial requested
                            CloudCover = openWeatherApiResponse.Clouds.CloudCover,
                            Temperature = openWeatherApiResponse.ObservationData.Temperature,
                            Humidity = openWeatherApiResponse.ObservationData.Humidity,
                            Pressure = openWeatherApiResponse.ObservationData.Pressure * 0.0295301,  // Pressure always comes back in millibars, even when imperial requested
                            WindSpeed = openWeatherApiResponse.WindData.Speed,
                            WindDirection = CompassDirection.GetDirection(openWeatherApiResponse.WindData.Degrees),
                            WindDirectionDegrees = openWeatherApiResponse.WindData.Degrees,
                            RainfallOneHour = (openWeatherApiResponse.Rain?.RainfallOneHour ?? 0.0) * 0.03937008
                        },
                        FetchTime = DateTime.Now
                    };

                    return currentConditions;
                }
            }
        }

        public async Task<LocationForecast> GetForecast(string location)
        {
            var response = await _weatherClient.GetForecast(location);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => await DeserializeObject(),
                HttpStatusCode.NotFound => LocationForecast.NotFound(location),
                HttpStatusCode.BadRequest => LocationForecast.BadRequest(),
                _ => LocationForecast.Error(),
            };

            async Task<LocationForecast> DeserializeObject()
            {
                var json = await response!.Content.ReadAsStringAsync();
                var locationForecast = JsonConvert.DeserializeObject<ForecastResponse>(json)!;
                return MapForecastResponse(locationForecast);

                LocationForecast MapForecastResponse(ForecastResponse openWeatherApiResponse)
                {
                    var locationForecast = new LocationForecast()
                    {
                        Success = true,
                        ErrorMessage = string.Empty,
                        Location = new ForecastLocation()
                        {
                            Name = openWeatherApiResponse.Location.Name,
                            Latitude = openWeatherApiResponse.Location.Coordinates.Latitude,
                            Longitude = openWeatherApiResponse.Location.Coordinates.Longitude
                        },
                        FetchTime = DateTime.Now
                    };

                    foreach (var openWeatherForecast in openWeatherApiResponse.ForecastPoints)
                    {
                        var forecast = new WeatherForecast()
                        {
                            Conditions = openWeatherForecast.Conditions.FirstOrDefault()?.main,
                            ConditionsDescription = openWeatherForecast.Conditions.FirstOrDefault()?.description,
                            Temperature = openWeatherForecast.WeatherData.Temperature,
                            Humidity = openWeatherForecast.WeatherData.Humidity,
                            Pressure = openWeatherForecast.WeatherData.pressure * 0.0295301,  // Pressure always comes back in millibars, even when imperial requested,
                            ForecastTime = DateTimeOffset.FromUnixTimeSeconds(openWeatherForecast.Date + openWeatherApiResponse.Location.TimezoneOffset).DateTime,
                            CloudCover = openWeatherForecast.Clouds.CloudCover,
                            WindSpeed = openWeatherForecast.Wind.WindSpeed,
                            WindDirectionDegrees = openWeatherForecast.Wind.WindDirectionDegrees,
                            WindDirection = CompassDirection.GetDirection(openWeatherForecast.Wind.WindDirectionDegrees),
                            ExpectedRainfall = (openWeatherForecast.Rain?.RainfallThreeHours ?? 0.0) * 0.03937008,
                            ExpectedSnowfall = (openWeatherForecast.Snow?.SnowfallThreeHours ?? 0.0) * 0.03937008
                        };
                        locationForecast.Forecasts.Add(forecast);
                    }

                    return locationForecast;
                }
            }
        }
    }
}
