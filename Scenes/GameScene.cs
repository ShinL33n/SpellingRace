using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    // JPWP: Scena odpowiadająca za wyświetlenie ekranu rozgrywki
    public class GameScene : Scene
    {
        private EnvironmentBackgroundManager _environmentBackgroundManager;
        private Background _background;
        private Player _player;

        private MovementManager _movementManager;
        private GateChoiceManager _gateChoiceManager;
        private GatesManager _gatesManager;
        private GameStateManager _gameStateManager;
        private GameInfoDisplayManager _gameInfoDisplayManager;
        private SettingsManager _settingsManager;
        private GameState _gameState;

        public GameScene(){}

        public override void LoadContent()
        {
            CreatePlayer();

            _gameStateManager = new();
            _gameStateManager.RegisterGameState();
            _settingsManager = ServiceProvider.Resolve<SettingsManager>() ?? throw new NullReferenceException("SettingsManager not registered in ServiceProvider.");
            _gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("GameState not registered in ServiceProvider.");

            _settingsManager.LoadSettings();
            _settingsManager.LoadGameStateParameters();

            _gatesManager = new();
            _gameInfoDisplayManager = new();
            _gateChoiceManager = new(_gatesManager);

            _environmentBackgroundManager = new();

            _background = _gameState.Difficulty switch {
                Difficulty.EASY => new(_content.Load<Texture2D>("Media/Backgrounds/GameBackgroundEasy")),
                Difficulty.NORMAL => new(_content.Load<Texture2D>("Media/Backgrounds/GameBackgroundNormal")),
                Difficulty.HARD => new(_content.Load<Texture2D>("Media/Backgrounds/GameBackgroundHard")),
                _ => new(_content.Load<Texture2D>("Media/Backgrounds/GameBackgroundNormal")),
            };
            
            _movementManager = new(_player.Position);
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if(InputManager.WasKeyTriggered(Keys.Escape)) 
                Game1.SceneManager.AddScene(new PauseScene());


            _movementManager.Update(gameTime);
            Vector2 playerPosition = _movementManager.GetPlayerPosition();

            _gameStateManager.Update(gameTime);
            _environmentBackgroundManager.Update();
            _gateChoiceManager.Update(gameTime, playerPosition);
            _player.Sprite.Update(playerPosition);
            _gatesManager.Update();
            _gameInfoDisplayManager.Update(gameTime);
        }

        public override void Draw()
        {
            _environmentBackgroundManager.Draw();
            _background.Draw();
            _gatesManager.Draw();
            _player.Sprite.Draw();
            _gameInfoDisplayManager.Draw();
            //_gateChoiceManager.Draw();
        }



        private void CreatePlayer()
        {
            _player = new Player { Size = new(135, 230) };
            _player.Position = new(windowCenter.X , windowSize.Y - _player.Size.X / 2);
            _player.Sprite = new(
                _player.Position,
                _player.Size,
                _content.Load<Texture2D>("Textures/Game/Player"),
                true
            );
        }
    }
}