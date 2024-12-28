
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Managers.GameplayManagers
{
    public class GameLoopManager
    {
        private GraphicsDeviceManager _graphics;
        private ContentManager _content;
        
        Vector2 windowSize, gateMaxSize, gateMinSize;

        readonly float perspectiveModifier = 2.43f;
        readonly int gatesQuantity = 3;

         
        private Gate _gate;
        private GameStateManager _gameStateManager;

        public GameLoopManager()
        {
            _graphics = ServiceProvider.Resolve<GraphicsDeviceManager>() ?? throw new NullReferenceException("GraphicsDeviceManager not registered in ServiceProvider.");
            _content = ServiceProvider.Resolve<ContentManager>() ?? throw new NullReferenceException("ContentManager not registered in ServiceProvider.");
            
            windowSize = new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            gateMaxSize = new(391f, 294f);
            gateMinSize = new(gateMaxSize.X / perspectiveModifier, gateMaxSize.Y / perspectiveModifier);

            CreateGates();

            _gameStateManager = new();
        }

        public void Update(GameTime gameTime)
        {
            _gate.Position = new(windowSize.X / 2 - _gate.Size.X / 2, _gate.Position.Y + (1 * _gameStateManager.GetSpeedMultiplier()));
            _gate.Size = (_gate.Size.X <= gateMaxSize.X && _gate.Size.Y <= gateMaxSize.Y) ? new(_gate.Size.X + _gameStateManager.GetSpeedMultiplier()*(gateMaxSize.X - gateMinSize.X)/windowSize.X, _gate.Size.Y + _gameStateManager.GetSpeedMultiplier()*(gateMaxSize.Y - gateMinSize.Y)/(windowSize.Y + gateMinSize.Y)) : gateMaxSize;
            _gate.Sprite.Update(_gate.Position, _gate.Size);

            if(_gate.Position.Y >= windowSize.Y) CreateGates();
        }
        
        public void Draw()
        {
            _gate.Sprite.Draw();
        }

        private void CreateGates() {
            _gate = new Gate
            {
                Position = new(windowSize.X / 2 - gateMinSize.X / 2, -1 * gateMinSize.Y),
                Size = new(gateMinSize.X, gateMinSize.Y),
                Word = "Pszczoła",
                IncorrectForms = ["Przczoła", "Pżczoła"]
            };

            _gate.Sprite = new Sprite(
                _gate.Position,
                _gate.Size,
                _content.Load<Texture2D>("Textures/Game/Gate"),
                _content.Load<SpriteFont>("Fonts/OpenDyslexicFont"),
                Color.White,
                _gate.Word,
                false
            );
        }
    }
}