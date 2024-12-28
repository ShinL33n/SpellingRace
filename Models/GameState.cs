using System;
using System.Timers;

namespace SpellingRace.Models
{
    public class GameState
    {    
        public TimeSpan Time { get; set; }
        public int Level { get; set; }
        public int GatesPassed { get; set; }
        public int Life { get; set; }
        public int Score { get; set; }
        public float SpeedMultiplier { get; set; }
    }
}