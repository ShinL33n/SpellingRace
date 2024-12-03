using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Managers
{
    public static class InputManager
    {
        public static Rectangle MouseCursor { get; private set; }
        public static Vector2 MousePosition { get; private set; }
        public static bool LeftMouseButtonClicked { get; private set; }


        private static KeyboardState _currentKeyState, _lastKeyState;
        private static MouseState _currentMouseState, _lastMouseState;

        public static bool WasKeyTriggered(Keys key)
        {
            return _currentKeyState.IsKeyDown(key) && _lastKeyState.IsKeyUp(key);
        }

        public static bool WasKeyReleased(Keys key)
        {
            return _currentKeyState.IsKeyUp(key) && _lastKeyState.IsKeyDown(key); 
        }

        public static bool IsKeyPressed(Keys key)
        {
            return _currentKeyState.IsKeyDown(key);
        }

        // public static bool LeftMouseButtonClicked() { 
        //     return _currentMouseState.LeftButton == ButtonState.Released && _currentMouseState.LeftButton == ButtonState.Pressed;
        // }

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