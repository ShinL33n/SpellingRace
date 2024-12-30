
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Managers.GameplayManagers
{
    public class GatesManager
    {
        private GraphicsDeviceManager _graphics;
        private ContentManager _content;
        //private GameStateManager _gameStateManager;
        private GameState _gameState;
        private WordsManager _wordsManager;

        Difficulty difficulty;

        Vector2 windowSize, gateMaxSize, gateMinSize;
        float perspectiveModifier;

        
        private Vector2[] _positions;
        private Dictionary<string, Gate> _gates;
        private List<string> _wordsList;

        
        public GatesManager()
        {
            _graphics = ServiceProvider.Resolve<GraphicsDeviceManager>() ?? throw new NullReferenceException("GraphicsDeviceManager not registered in ServiceProvider.");
            _content = ServiceProvider.Resolve<ContentManager>() ?? throw new NullReferenceException("ContentManager not registered in ServiceProvider.");
            //_gameStateManager = ServiceProvider.Resolve<GameStateManager>() ?? throw new NullReferenceException("GameStateManager not registered in ServiceProvider.");
            _gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("GameState not registered in ServiceProvider.");
            _wordsManager = new();
            
            windowSize = new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            difficulty = Difficulty.NORMAL; // DIFFICULTY LEVEL 

            switch (difficulty){
                case Difficulty.EASY:
                    // TO CHANGE
                    gateMaxSize = new(391f, 294f); 
                    perspectiveModifier = 2.43f;
                    gateMinSize = gateMaxSize / perspectiveModifier;
                    _positions = [
                        new Vector2(0, 0),
                        new Vector2(0, 1)
                    ];
                    break;


                case Difficulty.NORMAL:
                    gateMaxSize = new(391f, 294f);
                    perspectiveModifier = 2.43f;
                    gateMinSize = gateMaxSize / perspectiveModifier;
                    _positions = [
                        new Vector2(windowSize.X / 2 - 180 - gateMinSize.X / 2, -1 * gateMinSize.Y),
                        new Vector2(windowSize.X / 2 - gateMinSize.X / 2, -1 * gateMinSize.Y),
                        new Vector2(windowSize.X / 2 + 180 - gateMinSize.X / 2, -1 * gateMinSize.Y) 
                    ];
                    break;


                case Difficulty.HARD:
                    // TO CHANGE
                    gateMaxSize = new(391f, 294f); 
                    perspectiveModifier = 2.43f;
                    gateMinSize = gateMaxSize / perspectiveModifier;
                    _positions = [
                        new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector2(0, 0), 
                        new Vector2(0, 1)
                    ];
                    break;
                default:
                    throw new Exception("No difficulty level has been set.");
            }


            // _wordsList = _wordsManager.GetWords(difficulty, _gameStateManager.GetLevel());
            _wordsList = _wordsManager.GetWords(difficulty, _gameState.Level);
            _gates = CreateGates(_wordsList);

        }

        public Dictionary<string, Gate> CreateGates(List<string> words)
        {
            Dictionary<string, Gate> gates = new();

            Random random = new();
            List<string> shuffledWords = words.OrderBy(x => random.Next()).ToList();
            
            switch (difficulty){
                case Difficulty.EASY:
                    gates.Add("left", new Gate{
                        Position = _positions[0],
                        Size = gateMinSize,
                        Word = shuffledWords[0]
                    });
                    gates.Add("right", new Gate{
                        Position = _positions[1],
                        Size = gateMinSize,
                        Word = shuffledWords[1]
                    });
                    SpriteCreateHelper( gates["left"]);
                    SpriteCreateHelper( gates["right"]);
                    break;


                case Difficulty.NORMAL:
                    gates.Add("left", new Gate{
                        Position = _positions[0],
                        Size = gateMinSize,
                        Word = shuffledWords[0]
                    });
                    gates.Add("middle", new Gate{
                        Position = _positions[1],
                        Size = gateMinSize,
                        Word = shuffledWords[1]
                    });
                    gates.Add("right", new Gate{
                        Position = _positions[2],
                        Size = gateMinSize,
                        Word = shuffledWords[2]
                    });
                    SpriteCreateHelper( gates["left"]);
                    SpriteCreateHelper( gates["middle"]);
                    SpriteCreateHelper( gates["right"]);
                    break;


                case Difficulty.HARD:
                    gates.Add("left", new Gate{
                        Position = _positions[0],
                        Size = gateMinSize,
                        Word = shuffledWords[0]
                    });
                    gates.Add("middleLeft", new Gate{
                        Position = _positions[1],
                        Size = gateMinSize,
                        Word = shuffledWords[1]
                    });
                    gates.Add("middleRight", new Gate{
                        Position = _positions[2],
                        Size = gateMinSize,
                        Word = shuffledWords[2]
                    });
                    gates.Add("right", new Gate{
                        Position = _positions[3],
                        Size = gateMinSize,
                        Word = shuffledWords[3]
                    });
                    SpriteCreateHelper( gates["left"]);
                    SpriteCreateHelper( gates["middleLeft"]);
                    SpriteCreateHelper( gates["middleRight"]);
                    SpriteCreateHelper( gates["right"]);
                    break;

                default:
                    throw new Exception("No difficulty level has been set.");
            }

            _gates = gates != null ? gates : throw new NullReferenceException("No gates have been added.");

            return gates;
        }


        public void Update(){

            switch (difficulty){
                case Difficulty.EASY:
                    EasyDifficultyGatesMovement();
                    break;

                case Difficulty.NORMAL:
                    NormalDifficultyGatesMovement();
                    break;

                case Difficulty.HARD:
                    HardDifficultyGatesMovement();
                    break;

                default:
                    throw new Exception("No difficulty level has been set.");
            }

            if(_gates["left"].Position.Y >= windowSize.Y) {
                // _wordsList = _wordsManager.GetWords(difficulty, _gameStateManager.GetLevel());
                _wordsList = _wordsManager.GetWords(difficulty, _gameState.Level);
                _gates = CreateGates(_wordsList);
            }
        }

        public void Draw()
        {
            foreach(var gate in _gates.Values) gate.Sprite.Draw();
        }

        private void SpriteCreateHelper(Gate gate){
            gate.Sprite = new Sprite(
                gate.Position,
                gate.Size,
                _content.Load<Texture2D>("Textures/Game/Gate"),
                _content.Load<SpriteFont>("Fonts/InterFont"),
                Color.White,
                gate.Word,
                false
            );
        }

        private void EasyDifficultyGatesMovement()
        {
            // Left gate movement

            // Right gate movement

        }

        private void NormalDifficultyGatesMovement() 
        {
            float SpeedMultiplier = _gameState.SpeedMultiplier;

            // Left gate movement
            _gates["left"].Position = new(
                windowSize.X / 2 - 180 - _gates["left"].Size.X / 2 - (float)Math.Tan(13.64*Math.PI/180) * (_gates["left"].Position.Y + _gates["left"].Size.Y), 
                _gates["left"].Position.Y + (1 * SpeedMultiplier)
            );

            // Middle gate movement
            _gates["middle"].Position = new(windowSize.X / 2 - _gates["middle"].Size.X / 2, _gates["middle"].Position.Y + (1 * SpeedMultiplier));

            // Right gate movement
            _gates["right"].Position = new(
                windowSize.X / 2 + 180 - _gates["right"].Size.X / 2 + (float)Math.Tan(13.64*Math.PI/180) * (_gates["right"].Position.Y + _gates["right"].Size.Y), 
                _gates["right"].Position.Y + (1 * SpeedMultiplier)
            );

            foreach(var gate in _gates.Values) {
                gate.Size = (gate.Size.X <= gateMaxSize.X && gate.Size.Y <= gateMaxSize.Y) 
                ? new(gate.Size.X + SpeedMultiplier * (gateMaxSize.X - gateMinSize.X) / windowSize.X, 
                gate.Size.Y + SpeedMultiplier * (gateMaxSize.Y - gateMinSize.Y) / (windowSize.Y + gateMinSize.Y)) 
                : gateMaxSize;

                gate.Sprite.Update(gate.Position, gate.Size);
            }
        }

        private void HardDifficultyGatesMovement()
        {
            // Left gate movement

            // MiddleLeft gate movement

            // MiddleRight gate movement

            // Right gate movement

        }
    }
}