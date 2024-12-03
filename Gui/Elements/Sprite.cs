using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Gui.Elements
{
    public class Sprite
    {

        private Vector2 _position, _size, _textPosition;
        private Texture2D _texture;
        private SpriteFont _font;
        private string _text;
        private Rectangle _spriteBoundaries;
        private bool _alignToCenterPoint;


        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;

        /// <summary>
        /// Sprite without text
        /// </summary>
        public Sprite(Vector2 position, Vector2 size, Texture2D texture, bool alignToCenterPoint = false)
        {
            // DI
            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>();
            _graphicsDevice = ServiceProvider.Resolve<GraphicsDevice>();

            // Dimensions
            _position = alignToCenterPoint ? position - size / 2 : position;
            _size = size;

            _texture = texture;

            _spriteBoundaries = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y); 

            _alignToCenterPoint = alignToCenterPoint;
        }

        /// <summary>
        /// Sprite with text
        /// </summary>
        public Sprite(Vector2 position, Vector2 size, Texture2D texture, SpriteFont font, Color textColor, string text, bool alignToCenterPoint = false)
        {

            // DI
            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>();

            // Dimensions
            _position = alignToCenterPoint ? position - size / 2 : position;
            _size = size;

            // Texts
            _font = font;
            _text = text;
            _textPosition = _position + (size - font.MeasureString(text)) / 2;

            // Texture
            _texture = texture;

            _spriteBoundaries = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y); 

            _alignToCenterPoint = alignToCenterPoint;
        }


        public void Update(Vector2? position = null, Vector2? size = null)
        {
            _position = position ?? _position;
            _size = size ?? _size;

            _position = _alignToCenterPoint ? _position - _size / 2 : _position;

            _spriteBoundaries = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y); 
        }

        public void Draw()
        {
            _spriteBatch.Draw(_texture, _spriteBoundaries, Color.White);
        }
    }
}