using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpellingRace.Core
{
    public class Game1 : Game
    {

        public static SceneManager SceneManager;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //private GameStateManager _gameStateManager;
        private SettingsManager _settingsManager;


        public Game1()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics = new(this);
            SceneManager = new();
            //_gameStateManager = new();
            _settingsManager = new();

        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Adding necessary class instances to service container
            ServiceProvider.Register(GraphicsDevice);
            ServiceProvider.Register(Content);
            ServiceProvider.Register(_spriteBatch);
            ServiceProvider.Register(_graphics);
            ServiceProvider.Register(_settingsManager);
            //_gameStateManager.RegisterGameState();
           

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 1024;
            _graphics.ApplyChanges();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            SceneManager.AddScene(new MenuScene());
        }

        protected override void Update(GameTime gameTime)
        {
            SceneManager.Update(gameTime);
            //_gameStateManager.Update(gameTime);

            if(SceneManager.IsEmpty) Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);
           
            _spriteBatch.Begin();

            SceneManager.Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
