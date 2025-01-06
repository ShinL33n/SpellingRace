
namespace SpellingRace.Models
{
    public abstract partial class Scene
    {
        /// <summary>
        /// Loads content
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Update method called multiple times per second
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws content
        /// </summary>
        public abstract void Draw();

    }
}