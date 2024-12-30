using System;
using System.IO;
using System.Text.Json;

namespace SpellingRace.Managers
{
    public class SettingsManager
    {
        // private GameStateManager _gameStateManager;
        private GameState _gameState;

        private readonly string _filePath;
        private string _jsonContent;
        private Settings _settings;

        public SettingsManager()
        {
            //_gameStateManager = ServiceProvider.Resolve<GameStateManager>() ?? throw new NullReferenceException("GameStateManager not registered in ServiceProvider.");
            _gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("GameState not registered in ServiceProvider.");
            _filePath = Path.Combine(AppContext.BaseDirectory, "Content", "Resources/settings.json");
            _jsonContent = File.ReadAllText(_filePath);
        }
    
        public void SaveSettings(int difficulty)
        {
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
            _settings = JsonSerializer.Deserialize<Settings>(_jsonContent);

            _gameState.Difficulty = _settings.Setting[0].DifficultyLevel switch
            {
                0 => Difficulty.EASY,
                1 => Difficulty.NORMAL,
                2 => Difficulty.HARD,
                _ => Difficulty.EASY
            };

            switch(_gameState.Difficulty)
            {
                case Difficulty.EASY:
                    _gameState.Life = 6;
                    _gameState.SpeedMultiplier = 1f;
                    _gameState.PathsNumber = 2;
                    break;

                case Difficulty.NORMAL:
                    _gameState.Life = 5;
                    _gameState.SpeedMultiplier = 3f;
                    _gameState.PathsNumber = 2;
                    break;

                case Difficulty.HARD:
                    _gameState.Life = 4;
                    _gameState.SpeedMultiplier = 5f;
                    _gameState.PathsNumber = 2;
                    break;

                default:
                    _gameState.Life = 6;
                    _gameState.SpeedMultiplier = 1f;
                    _gameState.PathsNumber = 2;
                    break;
                }

        }
    }
}