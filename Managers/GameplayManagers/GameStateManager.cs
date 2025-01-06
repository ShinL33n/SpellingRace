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
                Level = 0,
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

            _gameState.Time += gameTime.ElapsedGameTime;

            if(_gameState.GatesPassed >= 10) {
                _gameState.Level++;
                _gameState.SpeedMultiplier *= 1.4f;
                _gameState.GatesPassed = 0;
            }
        }
    }
}
