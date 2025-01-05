using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    // JPWP: Scena odpowiadająca za wyświetlenie ekranu rozgrywki
    public class GameScene : Scene
    {
        //private Sprite _environmentBackground;
        private EnvironmentBackgroundManager _environmentBackgroundManager;
        private Background _background;
        private Player _player;

        //private Difficulty _difficulty;

        private MovementManager _movementManager;
        private GateChoiceManager _gateChoiceManager;
        private GatesManager _gatesManager;
        //private WordsManager _wordsManager;
        private GameStateManager _gameStateManager;
        private GameInfoDisplayManager _gameInfoDisplayManager;
        private GameState _gameState;
        private SettingsManager _settingsManager;

        public GameScene(){}

        public override void LoadContent()
        {
            CreatePlayer();

            //_difficulty = Difficulty.NORMAL; // FROM SETTINGS

            // _gameSettingsManager = new();
            // ServiceProvider.Register(_gameSettingsLoader);

                //_gameStateManager = new();
            //ServiceProvider.Register(_gameStateManager);
            _gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("GameState not registered in ServiceProvider.");
            _gameState = new GameState {
                Time = TimeSpan.Zero,
                Level = 0, 
                GatesPassed = 0,
                Score = 0,
            };
            ServiceProvider.Register(_gameState);

            _settingsManager = new();
            _settingsManager.LoadSettings();

            //_gameStateManager = new();
            //_gameStateManager.RegisterGameState();
            

            _gatesManager = new();
            //_wordsManager = new();
            _gameInfoDisplayManager = new();
            _gateChoiceManager = new(_gatesManager);

            //_environmentBackground = new(_content.Load<Texture2D>("Media/Backgrounds/ForestBackgroundRight"));
            // _environmentBackground = new Sprite(
            //     new Vector2(-218, -143), 
            //     new Vector2(879, 1167), 
            //     _content.Load<Texture2D>("Media/Backgrounds/ForestBackground"),
            //     false
            // );
            //_environmentBackgroundLeft = new(new Vector2(-218, -143), new Vector2(858, 1167), _content.Load<Texture2D>("Media/Backgrounds/ForestBackgroundLeft"));
            // _environmentBackgroundRight = new(new Vector2(windowSize.X / 2, -143), new Vector2(858, 1167), _content.Load<Texture2D>("Media/Backgrounds/ForestBackgroundRight"));
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

            Vector2 playerPosition = _movementManager.GetPlayerPosition();


            //_environmentBackground.Update();
            //_environmentBackgroundManager.Update();
            _environmentBackgroundManager.Update();
            _gateChoiceManager.Update(gameTime, playerPosition);
            _movementManager.Update(gameTime);
            _player.Sprite.Update(playerPosition);
            _gatesManager.Update();
            _gameInfoDisplayManager.Update(gameTime);
        }

        public override void Draw()
        {
            _environmentBackgroundManager.Draw();
            // _environmentBackgroundLeft.Draw();
            // _environmentBackgroundRight.Draw();
            //_environmentBackgroundManager.Draw();
            _background.Draw();
            _gatesManager.Draw();
            _player.Sprite.Draw();
            _gameInfoDisplayManager.Draw();
            _gateChoiceManager.Draw();
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