using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    // JPWP: Scena odpowiadająca za wyświetlenie ekranu rozgrywki
    public class GameScene : Scene
    {
        Texture2D gameBackground, playerTexture;

        private Background _background;
        private Sprite _playerSprite;


        private MovementManager _movementManager;


        public GameScene()
        {

        }

        public override void LoadContent()
        {
            gameBackground = _content.Load<Texture2D>("Media/Backgrounds/GameBackground");
            playerTexture = _content.Load<Texture2D>("Textures/Game/Player");

            Vector2 playerSize = new(135, 230);
            Vector2 playerPosition = new(windowCenter.X , windowSize.Y - playerSize.X / 2);

            _playerSprite = new(
                playerPosition,
                playerSize,
                playerTexture,
                true
            );
            
            _background = new(gameBackground);

            _movementManager = new(playerPosition);
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if(InputManager.WasKeyTriggered(Keys.Escape)) 
                Game1.SceneManager.AddScene(new PauseScene());

            _movementManager.Update(gameTime);
            _playerSprite.Update(_movementManager.GetPlayerPosition());
        }

        public override void Draw()
        {
            _background.Draw();
            _playerSprite.Draw();
        }
    }
}