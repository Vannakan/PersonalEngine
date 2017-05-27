using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine.Managers.ServiceLocator;
using Engine.Interfaces.InputManager;
using Engine.Interfaces.Render;
using System;
using Engine;
using Engine.Interfaces.Screen;

namespace GameCode
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spr;

        public static Game1 Instance;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Instance = this;
            this.Window.Title = "JGM Engine Alpha";
            Locator.Instance._Game1 = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Components.Add((IGameComponent)Locator.Instance);


            ///Set the mous to visible
            this.IsMouseVisible = true;
            Constants.g = GraphicsDevice;
            Random random = new Random();
            Constants.r = random;
            spr = new SpriteBatch(GraphicsDevice);
            Locator.Instance.InitializeServices(Content, spr);
            Locator.Instance.registerService<GraphicsDevice>(GraphicsDevice);

            Locator.Instance.getService<IScreenManager>().Add<MainMenu>("MainMenu");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {


            Locator.Instance.getService<IRenderManager>().spriteBatch = spr;
            Constants.cm = Content;
            Constants.debugBatch = spr;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice.Clear(Constants.colour);

            Locator.Instance.getService<IInputManager>().Update(gameTime);

            Locator.Instance.UpdateServices(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Locator.Instance.getService<IRenderManager>().Draw();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
