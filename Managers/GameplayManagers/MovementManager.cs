using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Managers.GameplayManagers
{
    /// <summary>
    /// Manager for player movemnt
    /// </summary>
    public class MovementManager
    {
        private float _playerSpeed;
        private Vector2 _playerPosition;

        /// <summary>
        /// MovementManager constructor
        /// </summary>
        /// <param name="playerPosition"></param>
        public MovementManager(Vector2 playerPosition)
        {
            _playerPosition = playerPosition;
            _playerSpeed = 700f;
        }

        /// <summary>
        /// Updates player position
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            float updatedPlayerSpeed = _playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            
            if (InputManager.IsKeyPressed(Keys.Left))
            {
                _playerPosition.X -= updatedPlayerSpeed;
            }
            
            if (InputManager.IsKeyPressed(Keys.Right))
            {
                _playerPosition.X += updatedPlayerSpeed;
            }
        }

        /// <summary>
        /// Return player position
        /// </summary>
        /// <returns>Vector2</returns>
        public Vector2 GetPlayerPosition()
        {
            return _playerPosition;
        }
    }
}