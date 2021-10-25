namespace DecoratorDesignPattern.OpenWeatherMap.Models
{
    public class LocationForecast
    {
        public LocationForecast()
        {
            Forecasts = new List<WeatherForecast>();
        }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public ForecastLocation Location { get; set; }

        public DateTime FetchTime { get; set; }

        public List<WeatherForecast> Forecasts { get; private set; }

        public static LocationForecast NotFound(string location) => new() { Success = false, ErrorMessage = $"Forecast data for {location} could not be found" };

        public static LocationForecast BadRequest() => new() { Success = false, ErrorMessage = "Bad request sent to the server.  Make sure you have an API key, otherwise debug through the code to see what went wrong" };

        public static LocationForecast Error() => new() { Success = false, ErrorMessage = "Error calling OpenWeatherMap API.  Debug through the code to see what went wrong" };
    }



    public class ForecastLocation
    {

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }


    public class WeatherForecast
    {
        public DateTime ForecastTime { get; set; }

        public DateTime ForecastTimeUtc { get; set; }

        public string Conditions { get; set; }

        public string ConditionsDescription { get; set; }


        public int CloudCover { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public double Humidity { get; set; }

        public double WindSpeed { get; set; }

        public CompassDirection WindDirection { get; set; }

        public double WindDirectionDegrees { get; set; }

        public double ExpectedRainfall { get; set; }

        public double ExpectedSnowfall { get; set; }

    }


}
