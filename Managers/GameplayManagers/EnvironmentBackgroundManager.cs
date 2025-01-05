using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Managers.GameplayManagers
{
    public class EnvironmentBackgroundManager
    {
        private GameState _gameState;
        private ContentManager _content;

        private Background _environmentBackgroundLeft, _environmentBackgroundRight;

        // private GraphicsDeviceManager _graphics;
        // private Sprite _environmentBackground;
        
        // Vector2 windowSize, windowCenter;
        // Vector2 backgroundPosition, backgroundSize;
        // Vector2 maxBackgroundPosition, maxBackgroundSize;
        // Vector2 initialBackgroundPosition, initialBackgroundSize;

        public EnvironmentBackgroundManager()
        {
            _gameState = _gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("GameState not registered in ServiceProvider.");
            _content = ServiceProvider.Resolve<ContentManager>() ?? throw new NullReferenceException("ContentManager not registered in ServiceProvider.");


            _environmentBackgroundLeft = new(
                new Vector2(-218, -143), 
                new Vector2(858, 1167), 
                GetCurrentLevelBackground()["left"]
            );

            _environmentBackgroundRight = new(
                new Vector2(640, -143), 
                new Vector2(858, 1167), 
                GetCurrentLevelBackground()["right"]
            );

            // _graphics = ServiceProvider.Resolve<GraphicsDeviceManager>() ?? throw new NullReferenceException("GraphicsDeviceManager not registered in ServiceProvider.");
            // _content = ServiceProvider.Resolve<ContentManager>() ?? throw new NullReferenceException("ContentManager not registered in ServiceProvider.");
            // _gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("ContentManager not registered in ServiceProvider.");

            // windowSize = new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            // windowCenter = new(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            // // backgroundPosition = new(-218, -143);
            // // backgroundSize = new(879, 1167);
            // maxBackgroundPosition = new(-8093, windowSize.Y);
            // maxBackgroundSize = new(12247, 16236);

            // initialBackgroundPosition = new(438.5f, -201.5f);
            // initialBackgroundSize = new(307, 407);

            // backgroundPosition = initialBackgroundPosition;
            // backgroundSize = initialBackgroundSize;

            // _environmentBackground = new Sprite(
            //     backgroundPosition, 
            //     backgroundSize, 
            //     _content.Load<Texture2D>("Media/Backgrounds/ForestBackground"),
            //     true
            // );
        }

        public void Update()
        {
            _environmentBackgroundLeft.Update(GetCurrentLevelBackground()["left"]);
            _environmentBackgroundRight.Update(GetCurrentLevelBackground()["right"]);
        }

        // public void Update()
        // {
        //     // float SpeedMultiplier = _gameState.SpeedMultiplier;

        //     // // Left background movement
        //     // backgroundPosition = new(
        //     //     initialBackgroundPosition.X - (float)Math.Tan(19.93*Math.PI/180) * (backgroundPosition.Y + backgroundSize.Y / 2), 
        //     //     backgroundPosition.Y + (1 * SpeedMultiplier)
        //     //     // backgroundPosition.Y + 1
        //     // );

        //     // _gates["left"].Position = new(
        //     //     windowSize.X / 2 - 135 - _gates["left"].Size.X / 2 - (float)Math.Tan(10.39*Math.PI/180) * (_gates["left"].Position.Y + _gates["left"].Size.Y), 
        //     //     _gates["left"].Position.Y + (1 * SpeedMultiplier)
        //     // );

        //     // // Right gate movement
        //     // _gates["right"].Position = new(
        //     //     windowSize.X / 2 + 135 - _gates["right"].Size.X / 2 + (float)Math.Tan(10.39*Math.PI/180) * (_gates["right"].Position.Y + _gates["right"].Size.Y), 
        //     //     _gates["right"].Position.Y + (1 * SpeedMultiplier)
        //     // );

            
        //     // backgroundSize = new(
        //     //     backgroundSize.X + SpeedMultiplier * (maxBackgroundSize.X - initialBackgroundSize.X) / 3219, 
        //     //     backgroundSize.Y + SpeedMultiplier * (maxBackgroundSize.Y - initialBackgroundSize.Y) / 9301
        //     // );

        //     // _environmentBackground.Update(backgroundPosition, backgroundSize);
            
        // }

        public void Draw()
        {
            _environmentBackgroundLeft.Draw();
            _environmentBackgroundRight.Draw();
        }

        private Dictionary<string, Texture2D> GetCurrentLevelBackground()
        {
            Dictionary<string, Texture2D> textures = new();
            
            Texture2D textureLeft = _gameState.Level switch
            {
                0 => _content.Load<Texture2D>("Media/Backgrounds/ForestBackgroundLeft"),
                1 => _content.Load<Texture2D>("Media/Backgrounds/SeaBackgroundLeft"),
                2 => _content.Load<Texture2D>("Media/Backgrounds/CityBackgroundLeft"),
                3 => _content.Load<Texture2D>("Media/Backgrounds/MeadowBackgroundLeft"),
                4 => _content.Load<Texture2D>("Media/Backgrounds/CloudsBackgroundLeft"),
                5 => _content.Load<Texture2D>("Media/Backgrounds/MushroomsBackgroundLeft"),
                _ => _content.Load<Texture2D>("Media/Backgrounds/ForestBackgroundLeft")
            };

            Texture2D textureRight = _gameState.Level switch
            {
                0 => _content.Load<Texture2D>("Media/Backgrounds/ForestBackgroundRight"),
                1 => _content.Load<Texture2D>("Media/Backgrounds/SeaBackgroundRight"),
                2 => _content.Load<Texture2D>("Media/Backgrounds/CityBackgroundRight"),
                3 => _content.Load<Texture2D>("Media/Backgrounds/MeadowBackgroundRight"),
                4 => _content.Load<Texture2D>("Media/Backgrounds/CloudsBackgroundRight"),
                5 => _content.Load<Texture2D>("Media/Backgrounds/MushroomsBackgroundRight"),
                _ => _content.Load<Texture2D>("Media/Backgrounds/ForestBackgroundRight")
            };

            textures.Add("left", textureLeft);
            textures.Add("right", textureRight);

            return textures;
        } 
    }
}