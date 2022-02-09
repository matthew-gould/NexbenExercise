using NexbenExercise.Models;
using System.Threading.Tasks;

namespace NexbenExercise.Services
{
    interface ILastFmApiService
    {
        public Task<Tracks> GetAllTracks(string apiKey);
    }
}
