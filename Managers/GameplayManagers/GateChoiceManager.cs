
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Managers.GameplayManagers
{
    public class GateChoiceManager
    {
        private GatesManager _gatesManager;
        private GameState _gameState;

        string lastWord;


        public GateChoiceManager(GatesManager gatesManager)
        {
            _gatesManager = gatesManager;
            _gameState = ServiceProvider.Resolve<GameState>();

            lastWord = string.Empty;
        }


        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            string correctWord = _gatesManager.GetCorrectWord();
            int correctWordSegment = _gatesManager.GetCorrectWordGateSegment();

            if (_gatesManager.GetGatesYPosition() >= playerPosition.Y && lastWord != correctWord)
            {
                int playerSegment = GetPlayerSegmentPosition(playerPosition.X);
                if(playerSegment == correctWordSegment)
                {
                    _gameState.GatesPassed++;
                    _gameState.Score++;
                    lastWord = correctWord;
                }
                else
                {
                    _gameState.GatesPassed++;
                    _gameState.Life--;
                    lastWord = correctWord;
                    if(_gameState.Life < 1) Game1.SceneManager.AddScene(new GameOverScene());
                }
            }
        }

        public void Draw()
        {
            // TO DO: Good / Bad choice text;
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
                    if(playerPosition <= 320)                              segment = 0;
                    else if(playerPosition > 320 && playerPosition <= 640) segment = 1;
                    else if(playerPosition > 640 && playerPosition <= 960) segment = 2;
                    else if(playerPosition > 960)                          segment = 3;
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