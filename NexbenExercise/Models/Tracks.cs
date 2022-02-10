using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NexbenExercise.Models
{
    public class Tracks
    {
        [JsonPropertyName("track")]
        public List<Track> Track { get; set; }
    }
}
