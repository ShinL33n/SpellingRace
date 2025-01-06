using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpellingRace.Models
{
    /// <summary>
    /// Scene model class
    /// </summary>
    public abstract partial class Scene
    {    
        /// <summary>
        /// Did user quit form scene property
        /// </summary>
        public bool SceneQuited { get; protected set; }

        /// <summary>
        /// MonoGame ContentManager 
        /// </summary>
        protected ContentManager _content;

        /// <summary>
        /// MonoGame GraphicsDeviceManager 
        /// </summary>
        protected GraphicsDeviceManager _graphics;

        /// <summary>
        /// MonoGame SpriteBatch 
        /// </summary>
        protected SpriteBatch _spriteBatch;

        /// <summary>
        /// Window size Vector2
        /// </summary>
        protected Vector2 windowSize;

        /// <summary>
        /// Window center Vector2
        /// </summary>
        protected Vector2 windowCenter;


        /// <summary>
        /// Font sprite
        /// </summary>
        protected SpriteFont interFont, openDyslexicFont;

        /// <summary>
        /// Scene title string
        /// </summary>
        protected string sceneTitle;

        /// <summary>
        /// Scene constructor
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public Scene() 
        { 
            _content = ServiceProvider.Resolve<ContentManager>() ?? throw new NullReferenceException("ContentManager not registered in ServiceProvider.");
            _graphics = ServiceProvider.Resolve<GraphicsDeviceManager>() ?? throw new NullReferenceException("GraphicsDeviceManager not registered in ServiceProvider.");
            _spriteBatch = ServiceProvider.Resolve<SpriteBatch>() ?? throw new NullReferenceException("SpriteBatch not registered in ServiceProvider.");

            SceneQuited = false;

            windowSize = new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            windowCenter = new(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            interFont = _content.Load<SpriteFont>("Fonts/InterFont");
            openDyslexicFont = _content.Load<SpriteFont>("Fonts/OpenDyslexicFont");

            LoadContent();
        }
    }
}