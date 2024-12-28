using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Gui.Elements
{
    public class Sprite
    {

        private Vector2 _position, _size, _textPosition, _textSize;
        private Texture2D _texture;
        private SpriteFont _font;
        private string _text;
        private Rectangle _spriteBoundaries;
        private bool _alignToCenterPoint;
        private Color _textColor;
        private float _scale;


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
            _textColor = textColor;

            _spriteBoundaries = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y); 

            _alignToCenterPoint = alignToCenterPoint;
        }


        public void Update(Vector2? position = null, Vector2? size = null)
        {
            _position = position ?? _position;
            _size = size ?? _size;

            _position = _alignToCenterPoint ? _position - _size / 2 : _position;

            _spriteBoundaries = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y); 

            if(_font != null && _text != null) {
                _textSize = _font.MeasureString(_text);
                _scale = (_size.X - 40f) / _textSize.X;
                _textPosition.X = _position.X + _size.X / 2;
                _textPosition.Y = _position.Y + _size.Y / 2;
            }
        }

        public void Draw()
        {
            _spriteBatch.Draw(_texture, _spriteBoundaries, Color.White);

            if(_font != null && _text != null) _spriteBatch.DrawString(_font, _text, _textPosition, Color.White, 0f, _textSize / 2, _scale, SpriteEffects.None, 0f);
        }
    }
}