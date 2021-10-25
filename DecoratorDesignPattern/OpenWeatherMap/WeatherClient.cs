using DecoratorDesignPattern.Infrastructore;

namespace DecoratorDesignPattern.OpenWeatherMap
{
    public class WeatherClient
    {
        private readonly HttpClient _httpClient;
        private readonly OpenWeatherSetting _openWeatherSetting;

        public WeatherClient(HttpClient httpClient, OpenWeatherSetting openWeatherSetting)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _openWeatherSetting = openWeatherSetting ?? throw new ArgumentNullException(nameof(openWeatherSetting));

            _httpClient.BaseAddress = new Uri(openWeatherSetting.Uri, UriKind.Absolute);
        }

        public async Task<HttpResponseMessage> GetCurrentWeather(string location)
        {
            try
            {
                var section = GetSection("weather", location);
                var response = await _httpClient.GetAsync(section);
                return response;
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseMessage> GetForecast(string location)
        {
            try
            {
                var section = GetSection("forecast", location);
                var response = await _httpClient.GetAsync(section);
                return response;
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        private string GetSection(string api, string location) =>
            $"{api}?q={location}&units=metric&appid={_openWeatherSetting.ApiKey}";
    }
}
