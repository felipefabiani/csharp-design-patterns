namespace DecoratorDesignPattern.OpenWeatherMap.Models
{
    public class CurrentWeather
    {

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public LocationData Location { get; set; }

        public DateTime ObservationTime { get; set; }

        public DateTime ObservationTimeUtc { get; set; }

        public WeatherData CurrentConditions { get; set; }

        public class LocationData
        {

            public string Name { get; set; }

            public double Latitude { get; set; }

            public double Longitude { get; set; }
        }


        public class WeatherData
        {
            public string Conditions { get; set; }

            public string ConditionsDescription { get; set; }

            public double Visibility { get; set; }

            public int CloudCover { get; set; }

            public double Temperature { get; set; }

            public double Pressure { get; set; }

            public double Humidity { get; set; }

            public double WindSpeed { get; set; }

            public CompassDirection WindDirection { get; set; }

            public double WindDirectionDegrees { get; set; }

            public double RainfallOneHour { get; set; }

        }


        public DateTime FetchTime { get; set; }

        public static CurrentWeather NotFound(string location) => new() { Success = false, ErrorMessage = $"Weather data for {location} could not be found" };
        public static CurrentWeather BadRequest() => new() { Success = false, ErrorMessage = "Bad request sent to the server.  Make sure you have an API key, otherwise debug through the code to see what went wrong" };
        public static CurrentWeather Error() => new() { Success = false, ErrorMessage = "Error calling OpenWeatherMap API.  Debug through the code to see what went wrong" };

    }





}
