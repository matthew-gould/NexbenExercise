using System.Text.Json.Serialization;

namespace NexbenExercise.Models
{
    class LastFmModel
    {
        [JsonPropertyName("tracks")]
        public Tracks Tracks { get; set; }
    }
}
