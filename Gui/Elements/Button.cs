using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Gui.Elements
{
    public class Button
    {
        private enum _buttonStates { IDLE, HOVER, ACTIVE }
        private _buttonStates _buttonState;

        private Vector2 _position, _size, _textPosition;
        private Texture2D _texture;
        private Color _idleColor, _hoverColor, _activeColor, _maskColor, _textColor;
        private SpriteFont _font;
        private string _text;
        private Rectangle _buttonBoundaries;


        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;


        /// <summary>
        /// Button without fill
        /// </summary>
        public Button(Vector2 position, Vector2 size, SpriteFont font, Color textColor, string text, Color idleColor, Color hoverColor, Color activeColor, bool alignToCenterPoint = false)
        {
            _buttonState = _buttonStates.IDLE;

            // DI
            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>();
            _graphicsDevice = ServiceProvider.Resolve<GraphicsDevice>();

            // Dimensions
            _position = alignToCenterPoint ? position - size / 2 : position;
            _size = size;

            // Texts
            _font = font;
            _text = text;
            _textPosition = _position + (size - font.MeasureString(text)) / 2;

            // Colors & Textures
            _textColor = textColor;
            _idleColor = idleColor;
            _hoverColor = hoverColor;
            _activeColor = activeColor;

            _texture = new Texture2D(_graphicsDevice, (int)size.X, (int)size.Y);
            _texture.SetData([Color.White]);

            _buttonBoundaries = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y); 
        }

        /// <summary>
        /// Button with texture fill
        /// </summary>
        public Button(Vector2 position, Vector2 size, Texture2D texture, SpriteFont font, Color textColor, string text, Color idleColor, Color hoverColor, Color activeColor, bool alignToCenterPoint = false)
        {
            _buttonState = _buttonStates.IDLE;

            // DI
            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>();

            // Dimensions
            _position = alignToCenterPoint ? position - size / 2 : position;
            _size = size;

            // Texts
            _font = font;
            _text = text;
            _textPosition = _position + (size - font.MeasureString(text)) / 2;

            // Colors & Textures
            _textColor = textColor;
            _idleColor = idleColor;
            _hoverColor = hoverColor;
            _activeColor = activeColor; 
            _texture = texture;

            _buttonBoundaries = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y); 
        }

        public bool Clicked()
        {
            return _buttonState == _buttonStates.ACTIVE;
        }

        public void Update()
        {
            _buttonState = _buttonStates.IDLE;

            if (InputManager.MouseCursor.Intersects(_buttonBoundaries))
            {
                _buttonState = _buttonStates.HOVER;
                if (InputManager.LeftMouseButtonClicked)
                {
                    _buttonState = _buttonStates.ACTIVE;
                }
            }

            // Color change based on button state
            _maskColor = _buttonState switch
            {
                _buttonStates.IDLE => _idleColor,
                _buttonStates.HOVER => _hoverColor,
                _buttonStates.ACTIVE => _activeColor,
                _ => Color.Red,
            };
        }

        public void Draw()
        {
            _spriteBatch.Draw(_texture, _buttonBoundaries, _maskColor);
            _spriteBatch.DrawString(_font, _text, _textPosition, _textColor);
        }
    }
}