namespace SyncStyle.OpenWeatherMaps
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(string city);
    }
}