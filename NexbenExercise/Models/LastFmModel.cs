using System.Text.Json.Serialization;

namespace NexbenExercise.Models
{
    public class LastFmModel
    {
        [JsonPropertyName("tracks")]
        public Tracks Tracks { get; set; }
    }
}
