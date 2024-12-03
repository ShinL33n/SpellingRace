using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpellingRace.Core
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public Game1()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Adding necessary class instances to service container
            ServiceProvider.Register(GraphicsDevice);
            ServiceProvider.Register(Content);
            ServiceProvider.Register(_spriteBatch);
            ServiceProvider.Register(_graphics);

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 1024;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        // Test buttons
        Texture2D exampleTestButtonTexture;
        SpriteFont arialFont;
        
        // for later use
        //private static readonly Dictionary<string, Button> _buttons = new();
        private static  Button[] _buttons;


        protected override void LoadContent()
        {
            arialFont = Content.Load<SpriteFont>("Fonts/testFont");
            exampleTestButtonTexture = Content.Load<Texture2D>("Textures/Gui/game_example_texture");
            _buttons = [ 
                new Button(
                    new Vector2(_graphics.PreferredBackBufferWidth / 2,
                    _graphics.PreferredBackBufferHeight / 2),
                    new Vector2(300, 100),
                    exampleTestButtonTexture,
                    arialFont,
                    Color.White,
                    "",
                    Color.White,
                    Color.Gray,
                    Color.Red,
                    true
                ),
                new Button (
                    new Vector2(_graphics.PreferredBackBufferWidth / 2,
                    _graphics.PreferredBackBufferHeight / 2 + 200),
                    new Vector2(300, 100),
                    exampleTestButtonTexture,
                    arialFont,
                    Color.White,
                    "",
                    Color.White,
                    Color.Gray,
                    Color.Red,
                    true
                )
            ];
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Update();

            foreach(var button in _buttons) { button.Update(); }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach(var button in _buttons) { button.Draw(); }

            base.Draw(gameTime);
        }
    }
}
