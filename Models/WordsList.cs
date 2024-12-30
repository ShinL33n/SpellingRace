using System.Text.Json.Serialization;

namespace SpellingRace.Models
{
    public class IncorrectForm
    {
        [JsonPropertyName("word")]
        public string Word { get; set; }
        
        [JsonPropertyName("incorrect_forms")]
        public List<string> IncorrectForms { get; set; }
    }

    public class Form
    {
        [JsonPropertyName("form")]
        public int FormNumber { get; set; }
        
        [JsonPropertyName("words")]
        public List<IncorrectForm> Words { get; set; }
    }

    public class DifficultyLevel
    {
        [JsonPropertyName("level")]
        public int Level { get; set; }
        
        [JsonPropertyName("forms")]
        public List<Form> Forms { get; set; }
    }

    public class WordsList
    {
        [JsonPropertyName("difficulty_levels")]
        public List<DifficultyLevel> DifficultyLevels { get; set; }
    }
}