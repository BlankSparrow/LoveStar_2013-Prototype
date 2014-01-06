#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace LoveStar_Point
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentManager content;
        BasicEffect basicEffect;
        

        KeyPress_Manager keyPressManager;
        KeyPress keyPress;
        Level level;
        List<Level> levels;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services, "Content");

            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 460;

            keyPressManager = new KeyPress_Manager();
            keyPress = new KeyPress();

            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
               (0, GraphicsDevice.Viewport.Width,     // left, right
                GraphicsDevice.Viewport.Height, 0,    // bottom, top
                0, 1);
            
            IsMouseVisible = true;
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
            levels = LoadLevels();
            level = levels[0];
            //player = new Player(this, graphics);
            base.Initialize();
        }

        private List<Level> LoadLevels()
        {
            List<Level> levels = new List<Level>();
            levels.Add(new Level_01(this, content));
            levels.Add(new Level_02(this, content));
            //levels.Add(new Level_03(this, content));
            //levels.Add(new Level_04(this, content));
            //levels.Add(new Level_05(this, content));
            //levels.Add(new Level_06(this, content));
            //levels.Add(new Level_07(this, content));
            //levels.Add(new Level_08(this, content));
            //levels.Add(new Level_09(this, content));
            //levels.Add(new Level_10(this, content));
            //levels.Add(new Level_11(this, content));
            //levels.Add(new Level_12(this, content));
            //levels.Add(new Level_13(this, content));

            return levels;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyPress = keyPressManager.HandleInput();
            if (level.levelChange.isTrue())
            {
                level = levels[level.levelChange.get()];
            }

            level.Update(gameTime, keyPress);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            basicEffect.CurrentTechnique.Passes[0].Apply();
            level.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
