using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Gui.Backgrounds
{
    /// <summary>
    /// Class for implementing game backgrounds
    /// </summary>
    public class Background
    {

        private Texture2D _texture;
        private Rectangle _backgroundBoundaries;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// Full screen background constructor
        /// </summary>
        /// <param name="texture"></param>
        /// <exception cref="NullReferenceException"></exception>
        public Background(Texture2D texture)
        {
            _graphics = ServiceProvider.Resolve<GraphicsDeviceManager>() ?? throw new NullReferenceException("GraphicsDeviceManager not registered in ServiceProvider.");
            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>() ?? throw new NullReferenceException("SpriteBatch not registered in ServiceProvider.");

            _texture = texture;
            _backgroundBoundaries = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// Background constructor with given position and size
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="texture"></param>
        /// <exception cref="NullReferenceException"></exception>
        public Background(Vector2 position, Vector2 size, Texture2D texture)
        {
            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>() ?? throw new NullReferenceException("SpriteBatch not registered in ServiceProvider.");

            _texture = texture;
            _backgroundBoundaries = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y); 
        }

        /// <summary>
        /// Update background texture
        /// </summary>
        /// <param name="texture"></param>
        public void Update(Texture2D texture)
        {
            _texture = texture;
        }

        /// <summary>
        /// Draw background
        /// </summary>
        public void Draw()
        {
            _spriteBatch.Draw(_texture, _backgroundBoundaries, Color.White);
        }
    }

}