
namespace SpellingRace.Managers
{
    /// <summary>
    /// Manager for handling different game scenes
    /// </summary>
    public class SceneManager
    {
        private readonly Stack<Scene> _scenesStack;

        /// <summary>
        /// Returns if there is no scene
        /// </summary>
        public bool IsEmpty { get { return _scenesStack.Count <= 0; } }

        /// <summary>
        /// SceneManager constructor
        /// Screates scenes stack
        /// </summary>
        public SceneManager()
        {
            _scenesStack = new();
        }

        /// <summary>
        /// Adds scene to the stack
        /// </summary>
        /// <param name="scene"></param>
        public void AddScene(Scene scene)
        {
            _scenesStack.Push(scene);
        }
        
        /// <summary>
        /// Removes scene from the stack
        /// </summary>
        public void RemoveCurrentScene()
        {
            _scenesStack.Pop();
        }

        /// <summary>
        /// Returns current scene
        /// </summary>
        /// <returns>Scene</returns>
        public Scene GetCurrentScene()
        {
            return _scenesStack.Peek();
        }

        /// <summary>
        /// Clears scenes stack and leaves MenuScene
        /// </summary>
        public void ClearScenes()
        {
            _scenesStack.Clear();
            _scenesStack.Push(new MenuScene());
        }

        /// <summary>
        /// Updates scenes
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if(!IsEmpty)
            {
                GetCurrentScene().Update(gameTime);     
                
                if(GetCurrentScene().SceneQuited)
                {
                    RemoveCurrentScene();
                }
            }
        }

        /// <summary>
        /// Draws scene
        /// </summary>
        public void Draw()
        {
            if(!IsEmpty)
            {
                GetCurrentScene().Draw();
            }
        }

    }
}