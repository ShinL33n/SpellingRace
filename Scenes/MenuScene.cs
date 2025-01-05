using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    // JPWP: Scena odpowiadająca za wyświetlenie menu głównego
    public class MenuScene : Scene
    {
    
        Texture2D menuOptionBorderTexture, menuBackgroundTexture, exitTexture;
        
        private Dictionary<string, Button> _buttons;
        private Background _background;


        public MenuScene(){}

        public override void LoadContent()
        {
            sceneTitle = "WYŚCIG Z ORTOGRAFIĄ";

            menuOptionBorderTexture = _content.Load<Texture2D>("Textures/Gui/MenuOptionBorder");
            menuBackgroundTexture = _content.Load<Texture2D>("Media/Backgrounds/MenuBackground");
            exitTexture = _content.Load<Texture2D>("Textures/Gui/Exit");

            _background = new(menuBackgroundTexture);

            AddButtons();
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if(InputManager.WasKeyTriggered(Keys.Escape)) SceneQuited = true;

            foreach(var button in _buttons.Values) { button.Update(); }


            if (_buttons["PlayButton"].Clicked())
            {
                Game1.SceneManager.AddScene(new GameScene());
            }
            if (_buttons["SettingsButton"].Clicked())
            {
                Game1.SceneManager.AddScene(new SettingsScene());
            }
            if (_buttons["HelpButton"].Clicked())
            {
                Game1.SceneManager.AddScene(new HelpScene());
            }
            if (_buttons["ExitButton"].Clicked())
            {
                SceneQuited = true;
            }
        }

        public override void Draw()
        {
            _background.Draw();

            foreach(var button in _buttons.Values) { button.Draw(); }
            
            _spriteBatch.DrawString(openDyslexicFont, sceneTitle, new Vector2(windowCenter.X - openDyslexicFont.MeasureString(sceneTitle).X / 2, 20), Color.White);
        }


        private void AddButtons()
        {
            Vector2 buttonSize = new(581, 116);
            Vector2 exitButtonSize = new(96, 96);

            _buttons = new() {
                 ["PlayButton"] = new Button(
                    new Vector2(windowCenter.X,
                                windowCenter.Y - 1.5f * buttonSize.Y),
                    buttonSize,
                    menuOptionBorderTexture,
                    interFont,
                    Color.White,
                    "Graj",
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    true
                ),
                ["SettingsButton"] = new Button(
                    new Vector2(windowCenter.X,
                                windowCenter.Y),
                    buttonSize,
                    menuOptionBorderTexture,
                    interFont,
                    Color.White,
                    "Ustawienia",
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    true
                ),
                ["HelpButton"] = new Button(
                    new Vector2(windowCenter.X,
                                windowCenter.Y + 1.5f * buttonSize.Y),
                    buttonSize,
                    menuOptionBorderTexture,
                    interFont,
                    Color.White,
                    "Pomoc",
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    true
                ),
                ["ExitButton"] = new Button(
                    windowSize - exitButtonSize,
                    exitButtonSize,
                    exitTexture,
                    interFont,
                    Color.White,
                    "",
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    false
                )
            };
        }
    }
}