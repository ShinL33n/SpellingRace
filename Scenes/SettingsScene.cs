
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    // JPWP: Scena odpowiadająca za wyświetlenie menu ustawień
    public class SettingsScene : Scene
    {
        private Background _background;
        private GameState _gameState;
        private SettingsManager _settingsManager;
        private Dictionary<string, Button> _buttons;

        Texture2D optionBorderTexture;
        string easyDifficultyText, normalDifficultyText,  hardDifficultyText, difficultyText;
        
        public SettingsScene()
        {
        }

        public override void LoadContent()
        {
            sceneTitle = "USTAWIENIA";

            optionBorderTexture = _content.Load<Texture2D>("Textures/Gui/OptionBorder");
            _background = new(_content.Load<Texture2D>("Media/Backgrounds/OptionsBackground"));
            _gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("GameState not registered in ServiceProvider.");
            _settingsManager = new();


            easyDifficultyText = "Łatwy";
            normalDifficultyText = "Średni";
            hardDifficultyText = "Trudny";
        
            

            AddButtons();
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if(InputManager.WasKeyTriggered(Keys.Escape)) SceneQuited = true;

            foreach(var button in _buttons.Values) button.Update();

            if (_buttons["easyDifficultyButton"].Clicked())
            {
                _settingsManager.SaveSettings(0);
                _gameState.Difficulty = Difficulty.EASY;
            }
            if (_buttons["normalDifficultyButton"].Clicked())
            {
                _settingsManager.SaveSettings(1);
                _gameState.Difficulty = Difficulty.NORMAL;
            }
            if (_buttons["hardDifficultyButton"].Clicked())
            {
                _settingsManager.SaveSettings(2);
                _gameState.Difficulty = Difficulty.HARD;
            }

            difficultyText = _gameState.Difficulty switch
            {
                Difficulty.EASY => easyDifficultyText,
                Difficulty.NORMAL => normalDifficultyText,
                Difficulty.HARD => hardDifficultyText,
                _ => easyDifficultyText
            };
        }

        public override void Draw()
        {
            _background.Draw();
            
            _spriteBatch.DrawString(openDyslexicFont, sceneTitle, new Vector2(windowCenter.X - openDyslexicFont.MeasureString(sceneTitle).X / 2, 20), Color.White);
            _spriteBatch.DrawString(interFont, "Poziom trudności:", new Vector2(60, 170), Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(interFont, "(Aktualnie wybrany: " + difficultyText + ")", new Vector2(60, 230), Color.White, 0f, Vector2.Zero, 0.35f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(interFont, "Uwaga! Wybrany poziom trudności zostanie zastosowany dopiero po", new Vector2(60, 900), Color.OrangeRed, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(interFont, "rozpoczęciu nowej gry.", new Vector2(60, 930), Color.OrangeRed, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

            foreach(var button in _buttons.Values) button.Draw();
        }


        private void AddButtons()
        {
            Vector2 buttonSize = new(200, 100);

             _buttons = new() {
                 ["easyDifficultyButton"] = new Button(
                    new(450, 171),
                    new(interFont.MeasureString(easyDifficultyText).X + 30, interFont.MeasureString(easyDifficultyText).Y + 20),
                    optionBorderTexture,
                    interFont,
                    Color.White,
                    easyDifficultyText,
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    false
                ),
                ["normalDifficultyButton"] = new Button(
                    new(700, 171),
                    new(interFont.MeasureString(normalDifficultyText).X + 30, interFont.MeasureString(normalDifficultyText).Y + 20),
                    optionBorderTexture,
                    interFont,
                    Color.White,
                    normalDifficultyText,
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    false
                ),
                ["hardDifficultyButton"] = new Button(
                    new(950, 171),
                    new(interFont.MeasureString(hardDifficultyText).X + 30, interFont.MeasureString(hardDifficultyText).Y + 20),
                    optionBorderTexture,
                    interFont,
                    Color.White,
                    hardDifficultyText,
                    Color.White,
                    Color.Gray,
                    Color.DarkGray,
                    false
                ),
             };
        }
    }
}