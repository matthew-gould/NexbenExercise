using Newtonsoft.Json;
using NexbenExercise.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NexbenExercise.Services
{
    public class LastFmApiService : ILastFmApiService
    {
        private static HttpClient _client = new HttpClient();

        public async Task<IOrderedEnumerable<KeyValuePair<string, ArtistCount>>> GetAllTracks(string apiKey)
        {
            var lastFmApi = $"https://ws.audioscrobbler.com/2.0/?method=chart.gettoptracks&api_key={apiKey}&format=json";

            var apiResult = await _client.GetAsync(lastFmApi);
            var stringResult = await apiResult.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LastFmModel>(stringResult);

            var sortedResult = SortArtists(result);

            return sortedResult;
        }

        public async Task<string> TayTayIsMyBae(string apiKey)
        {
            var tayTayApi = $"http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist=TaylorSwift&api_key={apiKey}&format=json";

            var tayResult = await _client.GetAsync(tayTayApi);
            var tayString = await tayResult.Content.ReadAsStringAsync();
            var tayBae = JsonConvert.DeserializeObject<TayTayBae>(tayString);

            return GetTopRankedSong(tayBae);
        }

        public string GetTopRankedSong(TayTayBae songList)
        {
            return songList.TopTracks?.Track?.Where(x => x.RankMeta.Rank == 1).FirstOrDefault().Name;
        }

        public IOrderedEnumerable<KeyValuePair<string, ArtistCount>> SortArtists(LastFmModel songList)
        {

            var artistList = new Dictionary<string, ArtistCount>();

            foreach (var track in songList.Tracks.Track)
            {
                if (artistList.ContainsKey(track.Artist.Name))
                {
                    artistList[track.Artist.Name].SongCount++;
                    artistList[track.Artist.Name].TotalPlaycount += track.Playcount;
                    artistList[track.Artist.Name].DivisibleByNine = artistList[track.Artist.Name].TotalPlaycount % 9 == 0;
                }
                else
                {
                    artistList.Add(track.Artist.Name,
                        new ArtistCount()
                        {
                            SongCount = 1,
                            TotalPlaycount = track.Playcount,
                            DivisibleByNine = track.Playcount % 9 == 0
                        }
                     );
                }
            }

            var sortedArtistList = artistList.OrderByDescending(x => x.Value.SongCount);
            return sortedArtistList;
        }
    }
}