
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    /// <summary>
    /// Game over screen class
    /// </summary>
    public class GameOverScene : Scene
    {
        private GameState _gameState;

        private Dictionary<string, Button> _buttons;

        Texture2D menuOptionBorderTexture;
        string endTime;

        /// <summary>
        /// Loads scene content
        /// </summary>
        public override void LoadContent()
        {
            sceneTitle = "KONIEC GRY";

            _gameState = ServiceProvider.Resolve<GameState>();
            menuOptionBorderTexture = _content.Load<Texture2D>("Textures/Gui/menuOptionBorder");

            endTime = $"{_gameState.Time.Minutes:D2}:{_gameState.Time.Seconds:D2}.";

            AddButtons();
        }

        /// <summary>
        /// Updates scene content
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if(InputManager.WasKeyTriggered(Keys.Escape)) 
                Game1.SceneManager.ClearScenes();

            foreach(var button in _buttons.Values) button.Update();

            if (_buttons["PlayAgainButton"].Clicked())
            {
                Game1.SceneManager.ClearScenes();
                Game1.SceneManager.AddScene(new GameScene());
            }
            if (_buttons["MainMenuButton"].Clicked())
            {
                Game1.SceneManager.ClearScenes();
            }
        }

        /// <summary>
        /// Draws screen content
        /// </summary>
        public override void Draw()
        {
            _spriteBatch.DrawString(
                openDyslexicFont, 
                sceneTitle, 
                new Vector2(windowCenter.X - openDyslexicFont.MeasureString(sceneTitle).X / 2, 20), 
                Color.White
            );

            string results = "Osiągnąłeś wynik " + _gameState.Score + " w czasie " + endTime;
            _spriteBatch.DrawString(
                interFont, 
                results, 
                new Vector2(windowCenter.X - interFont.MeasureString(results).X / 2, 200), 
                Color.White
            );

            foreach(var button in _buttons.Values) button.Draw();
        }

        /// <summary>
        /// Adds buttons to the scene
        /// </summary>
        private void AddButtons()
        {
            Vector2 buttonSize = new(581, 116);

            _buttons = new() {
                ["PlayAgainButton"] = new Button(
                    new Vector2(windowCenter.X,
                                windowCenter.Y),
                    buttonSize,
                    menuOptionBorderTexture,
                    interFont,
                    Color.White,
                    "Zagraj ponownie",
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    true
                ),
                ["MainMenuButton"] = new Button(
                    new Vector2(windowCenter.X,
                                windowCenter.Y + 1.5f * buttonSize.Y),
                    buttonSize,
                    menuOptionBorderTexture,
                    interFont,
                    Color.White,
                    "Menu Główne",
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    true
                )
            };
        }
    }
}