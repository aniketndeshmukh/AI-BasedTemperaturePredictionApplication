using RestSharp;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace AI_BasedTemperaturePredictionApplication.Services
{
    public class WeatherService
    {
        private readonly string apiKey = "21f20a96edf20fee5b6f4728664f6e5d";
        private readonly string baseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public async Task<float> GetTemperatureAsync(string city)
        {
            var client = new RestClient($"{baseUrl}?q={city}&appid={apiKey}&units=metric");
            var request = new RestRequest(); // No need to pass endpoint again

            var response = await client.ExecuteAsync(request, Method.Get); // Pass method here

            if (response.IsSuccessful)
            {
                var json = JObject.Parse(response.Content);
                return json["main"]["temp"].Value<float>();
            }
            return 0;
        }
    }
}
