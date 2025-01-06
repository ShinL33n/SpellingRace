using System;
using System.IO;
using System.Text.Json;

namespace SpellingRace.Managers
{
    /// <summary>
    /// Manager for handling game settings from json file
    /// </summary>
    public class SettingsManager
    {
        /// <summary>
        /// Game difficulty property
        /// </summary>
        public Difficulty Difficulty { get; set; } = Difficulty.EASY;

        private GameState _gameState;
        private Settings _settings;

        private readonly string _filePath;
        private string _jsonContent;

        /// <summary>
        /// SceneManager constructor
        /// </summary>
        public SettingsManager()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "Content", "Resources/settings.json");
        }

        /// <summary>
        /// Saves difficulty setting into json 
        /// </summary>
        /// <param name="difficulty"></param>
        public void SaveSettings(int difficulty)
        {
            _jsonContent = File.ReadAllText(_filePath);
            _settings = JsonSerializer.Deserialize<Settings>(_jsonContent);
            _settings.Setting[0].DifficultyLevel = difficulty;

            string updatedSettings = JsonSerializer.Serialize(_settings, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, updatedSettings);
        }

        /// <summary>
        /// Loads diffculty setting from json
        /// </summary>
        public void LoadSettings()
        {
            _jsonContent = File.ReadAllText(_filePath);
            _settings = JsonSerializer.Deserialize<Settings>(_jsonContent);

            Difficulty = _settings.Setting[0].DifficultyLevel switch
            {
                0 => Difficulty.EASY,
                1 => Difficulty.NORMAL,
                2 => Difficulty.HARD,
                _ => Difficulty.EASY
            };
        }

        /// <summary>
        /// Loads GameState parameters from loaded difficulty
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public void LoadGameStateParameters()
        {
            _gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("GameState not registered in ServiceProvider.");
            
            switch(Difficulty)
            {
                case Difficulty.EASY:
                    _gameState.Difficulty = Difficulty.EASY;
                    _gameState.Life = 6;
                    _gameState.SpeedMultiplier = 2f;
                    break;

                case Difficulty.NORMAL:
                    _gameState.Difficulty = Difficulty.NORMAL;
                    _gameState.Life = 5;
                    _gameState.SpeedMultiplier = 3f;
                    break;

                case Difficulty.HARD:
                    _gameState.Difficulty = Difficulty.HARD;
                    _gameState.Life = 4;
                    _gameState.SpeedMultiplier = 5f;
                    break;

                default:
                    _gameState.Difficulty = Difficulty.EASY;
                    _gameState.Life = 6;
                    _gameState.SpeedMultiplier = 1f;
                    break;
            }
        }
    }
}