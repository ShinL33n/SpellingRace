
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Managers.GameplayManagers
{
    public class GameLoopManager
    {
        //private GraphicsDeviceManager _graphics;
        //private ContentManager _content;
        
        //Vector2 windowSize, gateMaxSize, gateMinSize;

        //readonly float perspectiveModifier = 2.43f;
        //readonly static int gatesQuantity = 3;

         
        // private Gate[] _gate;

        private GatesManager _gatesManager;
        private WordsManager _wordsManager;
        private GameStateManager _gameStateManager;


        private Dictionary<string, Gate> _gates;
        private List<string> _wordsList;
        private Difficulty _difficulty;

        public GameLoopManager()
        {
            _wordsManager = new();
            _gatesManager = new();
            _gameStateManager = ServiceProvider.Resolve<GameStateManager>() ?? throw new NullReferenceException("GameStateManager not registered in ServiceProvider.");

            _difficulty = Difficulty.NORMAL; // FROM SETTINGS

            _wordsList = _wordsManager.GetWords(_difficulty, _gameStateManager.GetLevel());
            _gates = _gatesManager.CreateGates(_wordsList);
        }

        public void Update(GameTime gameTime)
        {
            _gatesManager.Update();
        }
        
        public void Draw()
        {
            _gatesManager.Draw();
        }
    }
}