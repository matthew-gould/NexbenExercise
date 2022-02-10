using NexbenExercise.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexbenExercise.Services
{
    interface ILastFmApiService
    {
        public Task<IOrderedEnumerable<KeyValuePair<string, ArtistCount>>> GetAllTracks(string apiKey);
        public Task<string> TayTayIsMyBae(string apiKey);
    }
}
