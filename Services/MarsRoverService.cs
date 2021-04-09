using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MarsRoverApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MarsRoverApp.Services
{
    public class MarsRoverService : IMarsRoverService
    {
        private static HttpClient _httpClient;
        private readonly ApiSettings _apiConfig;
        private readonly ILogger _logger;

        public MarsRoverService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options, ILogger<MarsRoverService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiConfig = options.Value;
            _logger = logger;
        }

        public async Task<MarsRoverApod> GetApod()
        {
            try
            {
                string url = _apiConfig.ApodBaseUrl;

                //Make Request
                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                //Deserialize the response
                var apodResponse = JsonSerializer.Deserialize<MarsRoverApod>(json);

                //return
                return apodResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetApod)} failed to get Apod from NASA API: {ex.Message} \n {ex.StackTrace}");
                throw new Exception($"Failed to get Astronomy Picture of the Day from NASA Api");
            }
        }

        private async Task DownloadAndSaveFile(string url)
        {
            try
            {
                //Get Filename from Url
                string filename = url.Split('/').Last();

                //Download the file
                var result = await _httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var image = await result.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync($"./Images/{filename}", image);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DownloadAndSaveFile)} failed to download and save the file from {url}: {ex.Message} \n {ex.StackTrace}");
            }

        }

        public async Task DownloadAndSaveApods()
        {
            try
            {
                string line;
                StreamReader file = new StreamReader(@".\Data\dates.txt");

                while ((line = file.ReadLine()) != null)
                {
                    try
                    {
                        //Get Apod by date
                        MarsRoverApod apod = await GetApodByDate(line);

                        //Download the file
                        await DownloadAndSaveFile(apod.Url);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{nameof(DownloadAndSaveApods)} failed to download and save file for input '{line}': {ex.Message} \n {ex.StackTrace}");
                        continue;
                    }

                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"{nameof(DownloadAndSaveApods)} input file missing: {ex.Message} \n {ex.StackTrace}");
                throw;
            }
        }

        public async Task<MarsRoverApod> GetApodByDate(string date)
        {
            //Check for null dates
            if (string.IsNullOrEmpty(date)) throw new ArgumentNullException(nameof(date), "Invalid Date");

            //Parse the date
            bool parsed = DateTime.TryParse(date.Trim(), out DateTime dt);

            //Check for valid date and format
            if (!parsed) throw new ArgumentException("Date must be valid and in YYYY-MM-DD format.", nameof(date));

            //Format date for API call
            date = dt.ToString("yyyy-MM-dd");

            //Get API Url
            string url = _apiConfig.ApodDateUrl(date);

            try
            {
                //Make Request
                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                //Deserialize the response
                var apodResponse = JsonSerializer.Deserialize<MarsRoverApod>(json);

                //return
                return apodResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError($"{nameof(GetApodByDate)} failed to get Apod from NASA API: {ex.Message} \n {ex.StackTrace}");
                throw new Exception($"Failed to get Astronomy Picture of the Day from NASA Api");
            }
        }
    }
}
