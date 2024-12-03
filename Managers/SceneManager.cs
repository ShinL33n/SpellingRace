using System.Collections.Generic;

namespace SpellingRace.Managers
{
    public class SceneManager
    {
        private readonly Stack<Scene> _scenesStack;

        public bool IsEmpty { get { return _scenesStack.Count <= 0; } }

        public SceneManager()
        {
            _scenesStack = new();
        }

        public void AddScene(Scene scene)
        {
            _scenesStack.Push(scene);
        }

        public void RemoveCurrentScene()
        {
            _scenesStack.Pop();
        }

        public Scene GetCurrentScene()
        {
            return _scenesStack.Peek();
        }

        public void ClearScenes()
        {
            _scenesStack.Clear();
            _scenesStack.Push(new MenuScene());
        }

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

        public void Draw()
        {
            if(!IsEmpty)
            {
                GetCurrentScene().Draw();
            }
        }

    }
}