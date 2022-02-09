using System.Text.Json.Serialization;

namespace NexbenExercise.Models
{
    class Track
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("playcount")]
        public string Playcount { get; set; }
        [JsonPropertyName("listeners")]
        public string Listeners { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("artist")]
        public Artist Artist { get; set; }
    }
}
