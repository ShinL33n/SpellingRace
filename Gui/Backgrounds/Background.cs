using System;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Gui.Backgrounds
{
    public class Background
    {

        private Texture2D _texture;
        private Rectangle _backgroundBoundaries;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Background(Texture2D texture)
        {
            _graphics = ServiceProvider.Resolve<GraphicsDeviceManager>() ?? throw new NullReferenceException("GraphicsDeviceManager not registered in ServiceProvider.");
            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>() ?? throw new NullReferenceException("SpriteBatch not registered in ServiceProvider.");

            _texture = texture;
            _backgroundBoundaries = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        public void Draw(){
            _spriteBatch.Draw(_texture, _backgroundBoundaries, Color.White);
        }
    }

}