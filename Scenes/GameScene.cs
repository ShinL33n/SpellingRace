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
        // private Player _player;


        private MovementManager _movementManager;
        private GameLoopManager _gameLoopManager;


        public GameScene()
        {

        }

        public override void LoadContent()
        {
            gameBackground = _content.Load<Texture2D>("Media/Backgrounds/GameBackground");
            playerTexture = _content.Load<Texture2D>("Textures/Game/Player");

            // _player = new Player{
            //     Size = new(135, 230),
            //     Position = new(windowCenter.X , windowSize.Y - _player.Size.X / 2),
            //     Sprite = new(
            //         _player.Position,
            //         _player.Size,
            //         _content.Load<Texture2D>("Textures/Game/Player"),
            //         true
            //     )
            // };

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
            //_movementManager = new(_player.Position);

            _gameLoopManager = new();
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if(InputManager.WasKeyTriggered(Keys.Escape)) 
                Game1.SceneManager.AddScene(new PauseScene());

            _movementManager.Update(gameTime);
            _playerSprite.Update(_movementManager.GetPlayerPosition());
            //_player.Sprite.Update(_movementManager.GetPlayerPosition());
            _gameLoopManager.Update(gameTime);
        }

        public override void Draw()
        {
            _background.Draw();
            _gameLoopManager.Draw();
            _playerSprite.Draw();
            //_player.Sprite.Draw();
        }
    }
}