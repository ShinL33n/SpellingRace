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
                Level = 1,
                GatesPassed = 0,
                Life = 5, // DIFFICULTY LEVEL PROPERTY
                Score = 0,
                SpeedMultiplier = 3f // DIFFICULTY LEVEL PROPERTY
            };
        }

        public void Update(GameTime gameTime)
        {
            if(_gameState.GatesPassed >= 10) {
                _gameState.Level++;
                _gameState.SpeedMultiplier *= 1.4f; // DIFFICULTY LEVEL PROPERTY
            }
        }

        public float GetSpeedMultiplier()
        {
            return _gameState.SpeedMultiplier;
        }

    }
}
