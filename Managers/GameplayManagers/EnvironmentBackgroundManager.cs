using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Managers.GameplayManagers
{
    public class EnvironmentBackgroundManager
    {
        private Background _environmentBackgroundLeft, _environmentBackgroundRight;

        private ContentManager _content;

        private GameState _gameState;


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
        }

        public void Update()
        {
            _environmentBackgroundLeft.Update(GetCurrentLevelBackground()["left"]);
            _environmentBackgroundRight.Update(GetCurrentLevelBackground()["right"]);
        }

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