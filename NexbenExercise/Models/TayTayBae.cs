using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NexbenExercise.Models
{
    public class TayTayBae
    {
        [JsonPropertyName("toptracks")]
        public TopTracks TopTracks { get; set; }
    }

    public class TopTracks
    {
        public List<Track> Track { get; set; }
    }

}
