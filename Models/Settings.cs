using System.Text.Json.Serialization;

namespace SpellingRace.Models
{
    public class DifficultySetting
    {
        [JsonPropertyName("difficulty")]
        public int DifficultyLevel { get; set; }
    }

    public class Settings
    {
        [JsonPropertyName("settings")]
        public List<DifficultySetting> Setting { get; set; }
    }
}