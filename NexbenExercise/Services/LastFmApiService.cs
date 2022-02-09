using Newtonsoft.Json;
using NexbenExercise.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace NexbenExercise.Services
{
    class LastFmApiService : ILastFmApiService
    {
        private HttpClient _client = new HttpClient();

        public async Task<Tracks> GetAllTracks(string apiKey)
        {
            var lastFmApi = $"https://ws.audioscrobbler.com/2.0/?method=chart.gettoptracks&api_key={apiKey}&format=json";

            var apiResult = await _client.GetAsync(lastFmApi);
            var stringResult = await apiResult.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LastFmModel>(stringResult);

            return new Tracks();
        }
    }
}
