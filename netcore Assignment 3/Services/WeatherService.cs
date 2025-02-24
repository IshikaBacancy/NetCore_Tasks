namespace netcore_Assignment3.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherService> _logger;
        private const string API_KEY = "b787e96305f957a66c5a65ddf1dfc75d"; 
        private const string BASE_URL = "https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API_KEY}&units=metric\r\n";

        public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GetWeatherAsync(double latitude, double longitude)
        {
            try
            {
                string url = $"{BASE_URL}?lat={latitude}&lon={longitude}&appid={API_KEY}&units=metric";

                var response = await _httpClient.GetStringAsync(url);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching weather data: {ex.Message}");
                return "Error retrieving weather data.";
            }
        }
    }
}
