using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingRace.Scenes
{
    // JPWP: Scena odpowiadająca za wyświetlenie pomocy
    public class HelpScene : Scene
    {
        private Background _background;

        public HelpScene()
        {

        }

        public override void LoadContent()
        {
            sceneTitle = "POMOC";

            _background = new(_content.Load<Texture2D>("Media/Backgrounds/OptionsBackground"));
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if(InputManager.WasKeyTriggered(Keys.Escape)) SceneQuited = true;
        }

        public override void Draw()
        {
            _background.Draw();

            _spriteBatch.DrawString(openDyslexicFont, sceneTitle, new Vector2(GetXStringCenter(sceneTitle, openDyslexicFont), 20), Color.White);
            _spriteBatch.DrawString(interFont, "Sterowanie:", new Vector2(GetXStringCenter("Sterowanie:", interFont), 130), Color.White);
            _spriteBatch.DrawString(interFont, "Esc - menu / cofnij / wyjście z gry", new Vector2(60, 200), Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(interFont, "< - ruch w lewo", new Vector2(60, 250), Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(interFont, "> - ruch w prawo", new Vector2(60, 300), Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(interFont, "Jak grać?", new Vector2(GetXStringCenter("Jak grać?", interFont), 370), Color.White);
            _spriteBatch.DrawString(interFont, "Poruszaj się po torze na boki i wchodź w bramki", new Vector2(60,440), Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(interFont, "z formą wyrazu, która wydaje ci się odpowiednia.", new Vector2(60,490), Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(interFont, "Wybranie dobrej formy skutkuje punktem, a złej utratą", new Vector2(60,540), Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(interFont, "życia. Utrata wszytkich żyć kończy grę.", new Vector2(60,590), Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
        }

        private float GetXStringCenter(string text, SpriteFont font)
        {
            return windowCenter.X - font.MeasureString(text).X / 2;
        }
    }
}