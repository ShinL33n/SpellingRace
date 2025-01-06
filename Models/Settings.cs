using System.Text.Json.Serialization;

namespace SpellingRace.Models
{
    /// <summary>
    /// Difficulty setting model
    /// </summary>
    public class DifficultySetting
    {
        /// <summary>
        /// Difficulty level property 
        /// </summary>
        [JsonPropertyName("difficulty")]
        public int DifficultyLevel { get; set; }
    }

    /// <summary>
    /// Settings model from json setting file
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Settings property list
        /// </summary>
        [JsonPropertyName("settings")]
        public List<DifficultySetting> Setting { get; set; }
    }
}