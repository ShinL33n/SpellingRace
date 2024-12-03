using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    // JPWP: Scena odpowiadająca za wyświetlenie pomocy
    public class HelpScene : Scene
    {
        public HelpScene()
        {

        }

        public override void LoadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if(InputManager.WasKeyTriggered(Keys.Escape)) SceneQuited = true;
        }

        public override void Draw()
        {
            
        }
    }
}