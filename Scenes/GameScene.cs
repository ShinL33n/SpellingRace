using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    // JPWP: Scena odpowiadająca za wyświetlenie ekranu rozgrywki
    public class GameScene : Scene
    {
        private Background _background;
        private Player _player;

        //private Difficulty _difficulty;

        private MovementManager _movementManager;
        private GatesManager _gatesManager;
        private WordsManager _wordsManager;
        private GameStateManager _gameStateManager;
        private GameInfoDisplayManager _gameInfoDisplayManager;
        //private GameState _gameState;
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
            //_gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("GameState not registered in ServiceProvider.");

            _settingsManager = new();
            _settingsManager.LoadSettings();
            
            _gatesManager = new();
            _wordsManager = new();
            _gameInfoDisplayManager = new();

            _background = new(_content.Load<Texture2D>("Media/Backgrounds/GameBackground"));
            _movementManager = new(_player.Position);
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if(InputManager.WasKeyTriggered(Keys.Escape)) 
                Game1.SceneManager.AddScene(new PauseScene());


            _movementManager.Update(gameTime);
            _player.Sprite.Update(_movementManager.GetPlayerPosition());
            _gatesManager.Update();
            //_gameStateManager.Update(gameTime);
            _gameInfoDisplayManager.Update(gameTime);
        }

        public override void Draw()
        {
            _background.Draw();
            _gatesManager.Draw();
            _player.Sprite.Draw();
            _gameInfoDisplayManager.Draw();
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