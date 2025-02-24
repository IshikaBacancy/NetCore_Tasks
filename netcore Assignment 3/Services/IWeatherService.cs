namespace netcore_Assignment3.Services
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(double latitude, double longitude);
    }
}
