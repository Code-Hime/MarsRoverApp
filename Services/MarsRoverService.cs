using System.Net.Http;
using System.Text.Json;
using MarsRoverApp.Models;

namespace MarsRoverApp.Services
{

    public interface IMarsRoverService
    {
        MarsRoverApod GetApod();
    }

    public class MarsRoverService : IMarsRoverService
    {
        private string _nasaAPIConfig = "SVaCdqwxqyG31lZEsjse7vSieZBYBn2mvS1jJoin";
        private static HttpClient _httpClient; 

        public MarsRoverService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public MarsRoverApod GetApod()
        {
            string url = $"https://api.nasa.gov/planetary/apod?api_key={ _nasaAPIConfig }";

            //Make Request
            var response = _httpClient.GetAsync(url).Result;
            var json = response.Content.ReadAsStringAsync().Result;

            //Deserialize the response
            var apodResponse = JsonSerializer.Deserialize<MarsRoverApod>(json);

            //return
            return apodResponse;
        }
    }
}
