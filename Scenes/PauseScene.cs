using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    // JPWP: Scena odpowiadająca za wyświetlenie menu pauzy w grze
    /// <summary>
    /// Pause screen class
    /// </summary>
    public class PauseScene : Scene 
    {
        private Dictionary<string, Button> _buttons;
        private Background _background;

        Texture2D menuOptionBorder, menuBackground;


        /// <summary>
        /// Loads content
        /// </summary>
        public override void LoadContent()
        {
            interFont = _content.Load<SpriteFont>("Fonts/InterFont");
            openDyslexicFont = _content.Load<SpriteFont>("Fonts/OpenDyslexicFont");

            menuOptionBorder = _content.Load<Texture2D>("Textures/Gui/MenuOptionBorder");
            menuBackground = _content.Load<Texture2D>("Media/Backgrounds/UniversalBackground");

            _background = new(menuBackground);

            Vector2 windowCenter = new(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            Vector2 buttonSize = new(581, 116);

            _buttons = new() {
                 ["ContinueButton"] = new Button(
                    new Vector2(windowCenter.X,
                                windowCenter.Y - 1.5f * buttonSize.Y),
                    buttonSize,
                    menuOptionBorder,
                    interFont,
                    Color.White,
                    "Kontynuuj",
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    true
                ),
                ["SettingsButton"] = new Button(
                    new Vector2(windowCenter.X,
                                windowCenter.Y),
                    buttonSize,
                    menuOptionBorder,
                    interFont,
                    Color.White,
                    "Ustawienia",
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    true
                ),
                ["QuitButton"] = new Button(
                    new Vector2(windowCenter.X,
                                windowCenter.Y + 1.5f * buttonSize.Y),
                    buttonSize,
                    menuOptionBorder,
                    interFont,
                    Color.White,
                    "Wyjdz do menu",
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    true
                )
            };
        }

        /// <summary>
        /// Updates content
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if(InputManager.WasKeyTriggered(Keys.Escape)) SceneQuited = true;

            foreach(var button in _buttons.Values) { button.Update(); }


            if (_buttons["ContinueButton"].Clicked())
            {
                Game1.SceneManager.RemoveCurrentScene();
            }
            if (_buttons["SettingsButton"].Clicked())
            {
                Game1.SceneManager.AddScene(new SettingsScene());
            }
            if (_buttons["QuitButton"].Clicked())
            {
                Game1.SceneManager.ClearScenes();
            }
        }

        /// <summary>
        /// Draws content
        /// </summary>
        public override void Draw()
        {
            _background.Draw();
            
            foreach(var button in _buttons.Values) { button.Draw(); }
        }
    }
}