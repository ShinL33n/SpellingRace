using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Models
{
    public abstract partial class Scene
    {    
        public bool SceneQuited { get; protected set; }

        protected ContentManager _content;
        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;

        protected Vector2 windowSize;
        protected Vector2 windowCenter;

        public Scene() 
        { 
            _content = ServiceProvider.Resolve<ContentManager>() ?? throw new NullReferenceException("ContentManager not registered in ServiceProvider.");
            _graphics = ServiceProvider.Resolve<GraphicsDeviceManager>() ?? throw new NullReferenceException("GraphicsDeviceManager not registered in ServiceProvider.");
            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>() ?? throw new NullReferenceException("SpriteBatch not registered in ServiceProvider.");

            SceneQuited = false;

            windowSize = new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            windowCenter = new(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            LoadContent();
        }

        // protected virtual void UnloadContent()
        // {
            
        // }

        // public virtual void End()
        // {
        
        // }
    }
}