using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Managers
{
    /// <summary>
    /// Manager for handling keyboard and mouse inputs
    /// </summary>
    public static class InputManager
    {
        /// <summary>
        /// Mouse cursor property
        /// </summary>
        public static Rectangle MouseCursor { get; private set; }

        /// <summary>
        /// Mouse position property
        /// </summary>
        public static Vector2 MousePosition { get; private set; }

        /// <summary>
        /// Was Left mouse button clicked property
        /// </summary>
        public static bool LeftMouseButtonClicked { get; private set; }


        private static KeyboardState _currentKeyState, _lastKeyState;
        private static MouseState _currentMouseState, _lastMouseState;

        /// <summary>
        /// Returns if the key was triggered
        /// </summary>
        /// <param name="key"></param>
        /// <returns>bool</returns>
        public static bool WasKeyTriggered(Keys key)
        {
            return _currentKeyState.IsKeyDown(key) && _lastKeyState.IsKeyUp(key);
        }

        /// <summary>
        /// Returns if the key was released
        /// </summary>
        /// <param name="key"></param>
        /// <returns>bool</returns>
        public static bool WasKeyReleased(Keys key)
        {
            return _currentKeyState.IsKeyUp(key) && _lastKeyState.IsKeyDown(key); 
        }

        /// <summary>
        /// Returns if the key is pressed
        /// </summary>
        /// <param name="key"></param>
        /// <returns>bool</returns>
        public static bool IsKeyPressed(Keys key)
        {
            return _currentKeyState.IsKeyDown(key);
        }

        // public static bool LeftMouseButtonClicked() { 
        //     return _currentMouseState.LeftButton == ButtonState.Released && _currentMouseState.LeftButton == ButtonState.Pressed;
        // }

        /// <summary>
        /// Updates keyboard and mouse actions
        /// </summary>
        public static void Update()
        {
            _lastKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();

            _lastMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            // JPWP: tymczasowe, niedoskonałe obsłużenie kliknięcia LPM
            LeftMouseButtonClicked = (_currentMouseState.LeftButton == ButtonState.Released) && (_lastMouseState.LeftButton == ButtonState.Pressed);

            MousePosition = new Vector2(_currentMouseState.Position.X, _currentMouseState.Position.Y);
            MouseCursor = new(_currentMouseState.Position, new(1, 1));
        }
    }
}