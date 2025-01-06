
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Managers.GameplayManagers
{
    /// <summary>
    /// Managers for gates behavior
    /// </summary>
    public class GatesManager
    {
        private GraphicsDeviceManager _graphics;
        private ContentManager _content;

        private WordsManager _wordsManager;
        private GameState _gameState;

        private Vector2[] _positions;
        private Dictionary<string, Gate> _gates;
        private List<string> _wordsList;

        float perspectiveModifier;
        Vector2 windowSize, gateMaxSize, gateMinSize;
        Difficulty difficulty;

        /// <summary>
        /// GatesManager constructor
        /// Sets gates and its properties based on game difficulty level
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="Exception"></exception>
        public GatesManager()
        {
            _graphics = ServiceProvider.Resolve<GraphicsDeviceManager>() ?? throw new NullReferenceException("GraphicsDeviceManager not registered in ServiceProvider.");
            _content = ServiceProvider.Resolve<ContentManager>() ?? throw new NullReferenceException("ContentManager not registered in ServiceProvider.");
            _gameState = ServiceProvider.Resolve<GameState>() ?? throw new NullReferenceException("GameState not registered in ServiceProvider.");
            _wordsManager = new();
            
            windowSize = new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            difficulty = _gameState.Difficulty;

            switch (difficulty){
                case Difficulty.EASY:
                    gateMaxSize = new(600, 451); 
                    perspectiveModifier = 2.35f;
                    gateMinSize = gateMaxSize / perspectiveModifier;
                    _positions = [
                        new Vector2(windowSize.X / 2 - 135 - gateMinSize.X / 2, -1 * gateMinSize.Y),
                        new Vector2(windowSize.X / 2 + 135 - gateMinSize.X / 2, -1 * gateMinSize.Y)
                    ];
                    break;


                case Difficulty.NORMAL:
                    gateMaxSize = new(391, 294);
                    perspectiveModifier = 2.43f;
                    gateMinSize = gateMaxSize / perspectiveModifier;
                    _positions = [
                        new Vector2(windowSize.X / 2 - 180 - gateMinSize.X / 2, -1 * gateMinSize.Y),
                        new Vector2(windowSize.X / 2 - gateMinSize.X / 2, -1 * gateMinSize.Y),
                        new Vector2(windowSize.X / 2 + 180 - gateMinSize.X / 2, -1 * gateMinSize.Y) 
                    ];
                    break;


                case Difficulty.HARD:
                    gateMaxSize = new(300, 225); 
                    perspectiveModifier = 2.4f;
                    gateMinSize = gateMaxSize / perspectiveModifier;
                    _positions = [
                        new Vector2(windowSize.X / 2 - 202.5f - gateMinSize.X / 2, -1 * gateMinSize.Y),
                        new Vector2(windowSize.X / 2 - 67.5f - gateMinSize.X / 2, -1 * gateMinSize.Y),
                        new Vector2(windowSize.X / 2 + 67.5f - gateMinSize.X / 2, -1 * gateMinSize.Y),
                        new Vector2(windowSize.X / 2 + 202.5f - gateMinSize.X / 2, -1 * gateMinSize.Y) 
                    ];
                    break;
                default:
                    throw new Exception("No difficulty level has been set.");
            }

            _wordsList = _wordsManager.GetWords(difficulty, _gameState.Level);
            _gates = CreateGates(_wordsList);

        }

        /// <summary>
        /// Creates gates
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public Dictionary<string, Gate> CreateGates(List<string> words)
        {
            Dictionary<string, Gate> gates = new();
            
            switch (difficulty){
                case Difficulty.EASY:
                    gates.Add("left", new Gate{
                        Position = _positions[0],
                        Size = gateMinSize,
                        Word = words[0]
                    });
                    gates.Add("right", new Gate{
                        Position = _positions[1],
                        Size = gateMinSize,
                        Word = words[1]
                    });
                    SpriteCreateHelper( gates["left"]);
                    SpriteCreateHelper( gates["right"]);
                    break;


                case Difficulty.NORMAL:
                    gates.Add("left", new Gate{
                        Position = _positions[0],
                        Size = gateMinSize,
                        Word = words[0]
                    });
                    gates.Add("middle", new Gate{
                        Position = _positions[1],
                        Size = gateMinSize,
                        Word = words[1]
                    });
                    gates.Add("right", new Gate{
                        Position = _positions[2],
                        Size = gateMinSize,
                        Word = words[2]
                    });
                    SpriteCreateHelper( gates["left"]);
                    SpriteCreateHelper( gates["middle"]);
                    SpriteCreateHelper( gates["right"]);
                    break;


                case Difficulty.HARD:
                    gates.Add("left", new Gate{
                        Position = _positions[0],
                        Size = gateMinSize,
                        Word = words[0]
                    });
                    gates.Add("middleLeft", new Gate{
                        Position = _positions[1],
                        Size = gateMinSize,
                        Word = words[1]
                    });
                    gates.Add("middleRight", new Gate{
                        Position = _positions[2],
                        Size = gateMinSize,
                        Word = words[2]
                    });
                    gates.Add("right", new Gate{
                        Position = _positions[3],
                        Size = gateMinSize,
                        Word = words[3]
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

        /// <summary>
        /// Upadates gates positions
        /// </summary>
        /// <exception cref="Exception"></exception>
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
                _wordsList = _wordsManager.GetWords(difficulty, _gameState.Level);
                _gates = CreateGates(_wordsList);
            }
        }

        /// <summary>
        /// Draws gates
        /// </summary>
        public void Draw()
        {
            foreach(var gate in _gates.Values) gate.Sprite.Draw();
        }

        /// <summary>
        /// Returns gates Y position
        /// </summary>
        /// <returns>float</returns>
        public float GetGatesYPosition()
        {
            return _gates["left"].Position.Y;
        }

        /// <summary>
        /// Returns segment of current word gate
        /// </summary>
        /// <returns>int</returns>
        public int GetCorrectWordGateSegment()
        {
            return _wordsList.IndexOf(_wordsManager.GetCorrectWord());
        }

        /// <summary>
        /// Returns correct word
        /// </summary>
        /// <returns>string</returns>
        public string GetCorrectWord()
        {
            return _wordsManager.GetCorrectWord();
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

        /// <summary>
        /// Manages gates movement for easy game difficulty
        /// </summary>
        private void EasyDifficultyGatesMovement()
        {
            float SpeedMultiplier = _gameState.SpeedMultiplier;

            // Left gate movement
            _gates["left"].Position = new(
                windowSize.X / 2 - 135 - _gates["left"].Size.X / 2 - (float)Math.Tan(10.39*Math.PI/180) * (_gates["left"].Position.Y + _gates["left"].Size.Y), 
                _gates["left"].Position.Y + (1 * SpeedMultiplier)
            );

            // Right gate movement
            _gates["right"].Position = new(
                windowSize.X / 2 + 135 - _gates["right"].Size.X / 2 + (float)Math.Tan(10.39*Math.PI/180) * (_gates["right"].Position.Y + _gates["right"].Size.Y), 
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

        /// <summary>
        /// Manages gates movement for normal game difficulty
        /// </summary>
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

        /// <summary>
        /// Manages gates movement for hard game difficulty
        /// </summary>
        private void HardDifficultyGatesMovement()
        {
            float SpeedMultiplier = _gameState.SpeedMultiplier;

            // Left gate movement
            _gates["left"].Position = new(
                windowSize.X / 2 - 202.5f - _gates["left"].Size.X / 2 - (float)Math.Tan(15.28*Math.PI/180) * (_gates["left"].Position.Y + _gates["left"].Size.Y), 
                _gates["left"].Position.Y + (1 * SpeedMultiplier)
            );

            // MiddleLeft gate movement
            _gates["middleLeft"].Position = new(
                windowSize.X / 2 - 67.5f - _gates["middleLeft"].Size.X / 2 - (float)Math.Tan(5.24*Math.PI/180) * (_gates["middleLeft"].Position.Y + _gates["middleLeft"].Size.Y), 
                _gates["middleLeft"].Position.Y + (1 * SpeedMultiplier)
            );

            // MiddleRight gate movement
            _gates["middleRight"].Position = new(
                windowSize.X / 2 + 67.5f - _gates["middleRight"].Size.X / 2 + (float)Math.Tan(5.24*Math.PI/180) * (_gates["middleRight"].Position.Y + _gates["middleRight"].Size.Y), 
                _gates["middleRight"].Position.Y + (1 * SpeedMultiplier)
            );

            // Right gate movement
            _gates["right"].Position = new(
                windowSize.X / 2 + 202.5f - _gates["right"].Size.X / 2 + (float)Math.Tan(15.28*Math.PI/180) * (_gates["right"].Position.Y + _gates["right"].Size.Y), 
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
    }
}