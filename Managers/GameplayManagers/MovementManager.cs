using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Managers.GameplayManagers
{
    public class MovementManager
    {
        private float _playerSpeed;
        private Vector2 _playerPosition;

        public MovementManager(Vector2 playerPosition)
        {
            _playerPosition = playerPosition;
            _playerSpeed = 700f;
        }

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

        public Vector2 GetPlayerPosition()
        {
            return _playerPosition;
        }
    }
}