using System.Text.Json.Serialization;

namespace SpellingRace.Models
{
    /// <summary>
    /// Incorect word form model
    /// </summary>
    public class IncorrectForm
    {
        /// <summary>
        /// Word property
        /// </summary>
        [JsonPropertyName("word")]
        public string Word { get; set; }
        
        /// <summary>
        /// Incorect forms property list
        /// </summary>
        [JsonPropertyName("incorrect_forms")]
        public List<string> IncorrectForms { get; set; }
    }

    /// <summary>
    /// Word form model
    /// </summary>
    public class Form
    {
        /// <summary>
        /// Number of word forms property
        /// </summary>
        [JsonPropertyName("form")]
        public int FormNumber { get; set; }

        /// <summary>
        /// Words property list
        /// </summary>
        [JsonPropertyName("words")]
        public List<IncorrectForm> Words { get; set; }
    }

    /// <summary>
    /// Difficulty level model
    /// </summary>
    public class DifficultyLevel
    {
        /// <summary>
        /// Level property
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }

        /// <summary>
        /// Words forms property list
        /// </summary>       
        [JsonPropertyName("forms")]
        public List<Form> Forms { get; set; }
    }

    /// <summary>
    /// Words list model from json setting file
    /// </summary>
    public class WordsList
    {
        /// <summary>
        /// Difficulty level property list
        /// </summary>
        [JsonPropertyName("difficulty_levels")]
        public List<DifficultyLevel> DifficultyLevels { get; set; }
    }
}