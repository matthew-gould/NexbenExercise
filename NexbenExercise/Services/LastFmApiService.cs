using Newtonsoft.Json;
using NexbenExercise.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NexbenExercise.Services
{
    class LastFmApiService : ILastFmApiService
    {
        private HttpClient _client = new HttpClient();

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

            return tayBae.TopTracks?.Track?.Where(x => x.RankMeta.Rank == 1).FirstOrDefault().Name;
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


//Print the artists who are in the Top-50, ordered from Most Songs in the Top 50 to Least.

//For the artists in the top half of step 1 (err on the side of more, so if there are 7 artists, 
//return 4 artists, for instance), return only those artists whose playcounts, when added together, are divisible by 9. Return in any order.

//If the above steps feel too easy, add the following catch for an edge case:
// If no playcounts can be divided by nine, get the top rated Taylor Swift song (see: https://www.last.fm/api/show/artist.getTopTracks -- HINT,
// her 'artist' entry is 'TaylorSwift') and print it out,
// as well as a message letting us know that none of the Step Two artists had playcounts divisible by 9.