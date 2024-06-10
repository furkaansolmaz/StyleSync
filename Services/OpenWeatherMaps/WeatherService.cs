
using Newtonsoft.Json.Linq;
using SyncStyle.OpenWeatherMaps;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<string> GetWeatherAsync(string city)
    {
        var response = await _httpClient.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");
        var weatherData = JObject.Parse(response);
        var description = weatherData["weather"][0]["description"].ToString();
        var temperature = weatherData["main"]["temp"].ToString();

        return $"Weather in {city}: {description}, {temperature}°C";
    }
}
