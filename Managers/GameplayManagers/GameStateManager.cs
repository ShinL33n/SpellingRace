using System;

namespace SpellingRace.Managers.GameplayManagers
{
    public class GameStateManager
    {
        private GameState _gameState;

        public GameStateManager()
        {
            _gameState = new GameState {
                Time = TimeSpan.Zero,
                Level = 3,
                GatesPassed = 0,
                Life = 0, // DIFFICULTY LEVEL PROPERTY
                Score = 0,
                SpeedMultiplier = 0 // DIFFICULTY LEVEL PROPERTY
            };

        }

        public void RegisterGameState()
        {
            ServiceProvider.Register(_gameState);
        }

        public void Update(GameTime gameTime)
        {
            // If it works it works, do not touch it \/
            _gameState = ServiceProvider.Resolve<GameState>();

            _gameState.Time += gameTime.ElapsedGameTime;

            if(_gameState.GatesPassed >= 10) {
                _gameState.Level++;
                _gameState.SpeedMultiplier *= 1.4f; // DIFFICULTY LEVEL PROPERTY
            }
        }

        // public float GetSpeedMultiplier()
        // {
        //     return _gameState.SpeedMultiplier;
        // }

        // public int GetLevel()
        // {
        //     return _gameState.Level;
        // }

        // public int GetLife()
        // {
        //     return _gameState.Life;
        // }

        // public int GetScore()
        // {
        //     return _gameState.Score;
        // }

        // public TimeSpan GetTime()
        // {
        //     return _gameState.Time;
        // }

    }
}
