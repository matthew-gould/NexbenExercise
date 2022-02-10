using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace NexbenExercise.Models
{
    class Track
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("playcount")]
        public int Playcount { get; set; }
        [JsonPropertyName("listeners")]
        public int Listeners { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("artist")]
        public Artist Artist { get; set; }
        [JsonProperty("@attr")]
        [JsonPropertyName("@attr")]
        public Meta RankMeta { get; set; }
    }

    class Meta
    {
        [JsonPropertyName("rank")]
        public int Rank { get; set; }
    }
}
