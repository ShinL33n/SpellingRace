using System;
using System.IO;
using System.Text.Json;

namespace SpellingRace.Managers
{
    public class SettingsManager
    {
        public Difficulty Difficulty { get; set; } = Difficulty.EASY;

        private GameState _gameState;
        private Settings _settings;

        private readonly string _filePath;
        private string _jsonContent;


        public SettingsManager()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "Content", "Resources/settings.json");
        }
    
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

        public void LoadGameStateParameters()
        {
            _gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("GameState not registered in ServiceProvider.");
            
            switch(Difficulty)
            {
                case Difficulty.EASY:
                    _gameState.Difficulty = Difficulty.EASY;
                    _gameState.Life = 6;
                    _gameState.SpeedMultiplier = 2f;
                    _gameState.PathsNumber = 2;
                    break;

                case Difficulty.NORMAL:
                    _gameState.Difficulty = Difficulty.NORMAL;
                    _gameState.Life = 5;
                    _gameState.SpeedMultiplier = 3f;
                    _gameState.PathsNumber = 2;
                    break;

                case Difficulty.HARD:
                    _gameState.Difficulty = Difficulty.HARD;
                    _gameState.Life = 4;
                    _gameState.SpeedMultiplier = 5f;
                    _gameState.PathsNumber = 2;
                    break;

                default:
                    _gameState.Difficulty = Difficulty.EASY;
                    _gameState.Life = 6;
                    _gameState.SpeedMultiplier = 1f;
                    _gameState.PathsNumber = 2;
                    break;
            }
        }
    }
}