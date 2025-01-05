
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Managers.GameplayManagers
{
    public class GateChoiceManager
    {
        private GraphicsDeviceManager _graphics;
        private ContentManager _content;

        private SpriteBatch _spriteBatch;
        private GameState _gameState;
        private GatesManager _gatesManager;

        Vector2 windowCenter, centerText;
        SpriteFont openDyslexicFont;
        string lastWord, choiceString;
        bool goodChoice;

        public GateChoiceManager(GatesManager gatesManager)
        {
            _gatesManager = gatesManager;

            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>();
            _graphics = ServiceProvider.Resolve<GraphicsDeviceManager>();
            _gameState = ServiceProvider.Resolve<GameState>();
            _content = ServiceProvider.Resolve<ContentManager>();


            windowCenter = new(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            openDyslexicFont = _content.Load<SpriteFont>("Fonts/OpenDyslexicFont");
            lastWord = string.Empty;
            goodChoice = true;
            //choiceString = "DOBRZE";

            // centerText = new Vector2(
            //     windowCenter.X - openDyslexicFont.MeasureString(choiceString).X / 2, 
            //     windowCenter.Y - - openDyslexicFont.MeasureString(choiceString).Y / 2
            // );
        }


        public void Update(GameTime gameTime, Vector2 playerPosition)//, float gatesYPosition, int correctSegment, string correctWord)
        {
            string correctWord = _gatesManager.GetCorrectWord();
            int correctWordSegment = _gatesManager.GetCorrectWordGateSegment();

            if (_gatesManager.GetGatesYPosition() >= playerPosition.Y && lastWord != correctWord) // BUG: if new drawed word is the same as the previous one then nothing happens (with the score/life)
            {
                if(GetPlayerSegmentPosition(playerPosition.X) == correctWordSegment)
                {
                    _gameState.GatesPassed++;
                    _gameState.Score++;
                    lastWord = correctWord;
                    goodChoice = true;
                    //choiceString = "DOBRZE";
                }
                else
                {
                    _gameState.GatesPassed++;
                    _gameState.Life--;
                    lastWord = correctWord;
                    goodChoice = false;
                    if(_gameState.Life < 1) Game1.SceneManager.AddScene(new GameOverScene());
                    //choiceString = "ŹLE";
                }

                // centerText = new Vector2(
                //     windowCenter.X - openDyslexicFont.MeasureString(choiceString).X / 2, 
                //     windowCenter.Y + openDyslexicFont.MeasureString(choiceString).Y / 2
                // );
            }
        }

        public void Draw()
        {
            //_spriteBatch.DrawString(openDyslexicFont, choiceString, centerText, Color.LimeGreen, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

        }

        private int GetPlayerSegmentPosition(float playerPosition)
        {
            int segment = 0;

            switch(_gameState.Difficulty)
            {
                case Difficulty.EASY:
                    if(playerPosition <= 640)     segment = 0;
                    else if(playerPosition > 640) segment = 1;
                    break;

                case Difficulty.NORMAL:
                    if(playerPosition <= 427)                              segment = 0;
                    else if(playerPosition > 427 && playerPosition <= 853) segment = 1;
                    else if(playerPosition > 853)                          segment = 2;
                    break;
                
                case Difficulty.HARD:
                    if(playerPosition <= 427)                              segment = 0;
                    else if(playerPosition > 427 && playerPosition <= 853) segment = 1;
                    else if(playerPosition > 427 && playerPosition <= 853) segment = 2;
                    else if(playerPosition > 853)                          segment = 3;
                    break;
                
                default:
                    if(playerPosition <= 640)     segment = 0;
                    else if(playerPosition > 640) segment = 1;
                    break;
                    
            }

            return segment;
        }
    }
}