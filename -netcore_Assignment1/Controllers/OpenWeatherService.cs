namespace _netcore_Assignment1.Controllers
{
    public class OpenWeatherService
    {
        public object GetClosestStationInfo(double latitude, double longitude)
        {
            // Try to get the closest weather station
            if (!OpenWeather.StationDictionary.TryGetClosestStation(latitude, longitude, out var stationInfo))
            {
                throw new Exception("Could not find a station.");
            }

            // Return only the required fields
            return new
            {
                Latitude = stationInfo.Latitude,
                Longitude = stationInfo.Longitude,
                Country = stationInfo.Country
            };
        }
    }
}
