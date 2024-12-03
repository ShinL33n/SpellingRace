using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Models
{
    public abstract partial class Scene
    {    
        protected ContentManager _content;
        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;

        public bool SceneQuited { get; protected set; }

        public Scene() 
        { 
            _content = ServiceProvider.Resolve<ContentManager>() ?? throw new NullReferenceException("ContentManager not registered in ServiceProvider.");
            _graphics = ServiceProvider.Resolve<GraphicsDeviceManager>() ?? throw new NullReferenceException("GraphicsDeviceManager not registered in ServiceProvider.");
            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>() ?? throw new NullReferenceException("SpriteBatch not registered in ServiceProvider.");

            SceneQuited = false;

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