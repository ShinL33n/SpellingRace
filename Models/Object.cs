

namespace SpellingRace.Models
{
    /// <summary>
    /// Model for game objects
    /// </summary>
    public class Object
    {    
        /// <summary>
        /// Object position property
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Object size property
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// Object sprite property
        /// </summary>
        public Sprite Sprite { get; set; }
    }
}