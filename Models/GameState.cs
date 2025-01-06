using System;

namespace SpellingRace.Models
{
    /// <summary>
    /// Game state model class
    /// </summary>
    public class GameState
    {   
        /// <summary>
        /// Ingame time passed property
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Current game level property
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Passed gates property
        /// </summary>
        public int GatesPassed { get; set; }

        /// <summary>
        /// Player life property
        /// </summary>
        public int Life { get; set; }

        /// <summary>
        /// Player score property
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Game speed property
        /// </summary>
        public float SpeedMultiplier { get; set; }

        /// <summary>
        /// Game difficulty property
        /// </summary>
        public Difficulty Difficulty { get; set; }
    }
}