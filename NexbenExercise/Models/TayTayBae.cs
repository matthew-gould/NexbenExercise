using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NexbenExercise.Models
{
    class TayTayBae
    {
        [JsonPropertyName("toptracks")]
        public TopTracks TopTracks { get; set; }
    }

    class TopTracks
    {
        public List<Track> Track { get; set; }
    }

}
