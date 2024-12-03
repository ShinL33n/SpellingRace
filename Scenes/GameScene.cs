using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    // JPWP: Scena odpowiadająca za wyświetlenie ekranu rozgrywki
    public class GameScene : Scene
    {
        Texture2D gameBackground;
        private Background _background;


        public GameScene(){}

        public override void LoadContent()
        {
            gameBackground = _content.Load<Texture2D>("Media/Backgrounds/GameBackground");
            
            _background = new(gameBackground);
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if(InputManager.WasKeyTriggered(Keys.Escape)) 
                Game1.SceneManager.AddScene(new PauseScene());
            
        }

        public override void Draw()
        {
            _background.Draw();
        }
    }
}