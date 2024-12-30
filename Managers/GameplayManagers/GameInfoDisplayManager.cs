
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Managers.GameplayManagers
{
    public class GameInfoDisplayManager
    {
        private GraphicsDeviceManager _graphics;
        private ContentManager _content;
        //private GameStateManager _gameStateManager;
        private GameState _gameState;

        private Sprite _ingameMenu;
        private SpriteBatch _spriteBatch;
        private TimeSpan _timer;
        private int _life, _score, _level;

        Vector2 windowSize;
        SpriteFont font;


        public GameInfoDisplayManager()
        {
            _graphics = ServiceProvider.Resolve<GraphicsDeviceManager>() ?? throw new NullReferenceException("GraphicsDeviceManager not registered in ServiceProvider.");
            _content = ServiceProvider.Resolve<ContentManager>() ?? throw new NullReferenceException("ContentManager not registered in ServiceProvider.");
            //_gameStateManager = ServiceProvider.Resolve<GameStateManager>() ?? throw new NullReferenceException("GameStateManager not registered in ServiceProvider.");
            _gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("GameStateManager not registered in ServiceProvider.");
            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>();

            windowSize = new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            font = _content.Load<SpriteFont>("Fonts/InterFont");
           
            _ingameMenu = new Sprite(
                new(0,0),
                new(windowSize.X, 110),
                _content.Load<Texture2D>("Textures/Game/IngameMenu"),
                false
            );

            // _life = _gameStateManager.GetLife();
            // _score = _gameStateManager.GetScore();
            // _level = _gameStateManager.GetLevel(); 
            // _timer = _gameStateManager.GetTime();
            _life = _gameState.Life;
            _score = _gameState.Score;
            _level = _gameState.Level;
            _timer = _gameState.Time;
        }

        public void Update(GameTime gameTime)
        {
            // _life = _gameStateManager.GetLife();
            // _score = _gameStateManager.GetScore();
            // _level = _gameStateManager.GetLevel(); 
            // _timer = _gameStateManager.GetTime();
            _life = _gameState.Life;
            _score = _gameState.Score;
            _level = _gameState.Level;
            _timer = _gameState.Time;

            _ingameMenu.Update();
        }

        public void Draw()
        {
            _ingameMenu.Draw();

            _spriteBatch.DrawString(font, $"{_timer.Minutes:D2}:{_timer.Seconds:D2}", new Vector2(1125,25), Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(font, _level.ToString(), new Vector2(885,25), Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(font, _score.ToString(), new Vector2(590,25), Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(font, _life.ToString(), new Vector2(301,37), Color.White, 0f, Vector2.Zero, 0.45f, SpriteEffects.None, 0f);
        }

    }
}