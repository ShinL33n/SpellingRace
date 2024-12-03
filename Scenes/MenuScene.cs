using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    // JPWP: Scena odpowiadająca za wyświetlenie menu głównego
    public class MenuScene : Scene
    {
    
        Texture2D menuOptionBorder, menuBackground;
        SpriteFont interFont, openDyslexicFont;
        
        private Dictionary<string, Button> _buttons;
        private Background _background;


        public MenuScene(){}

        public override void LoadContent()
        {
            interFont = _content.Load<SpriteFont>("Fonts/InterFont");
            openDyslexicFont = _content.Load<SpriteFont>("Fonts/OpenDyslexicFont");

            menuOptionBorder = _content.Load<Texture2D>("Textures/Gui/MenuOptionBorder");
            menuBackground = _content.Load<Texture2D>("Media/Backgrounds/MenuBackground");

            _background = new(menuBackground);

            
            Vector2 buttonSize = new(581, 116);

            _buttons = new() {
                 ["PlayButton"] = new Button(
                    new Vector2(windowCenter.X,
                                windowCenter.Y - 1.5f * buttonSize.Y),
                    buttonSize,
                    menuOptionBorder,
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
                    menuOptionBorder,
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
                    menuOptionBorder,
                    interFont,
                    Color.White,
                    "Pomoc",
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    true
                )
            };
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
        }

        public override void Draw()
        {
            _background.Draw();
            
            foreach(var button in _buttons.Values) { button.Draw(); }
        }
    }
}