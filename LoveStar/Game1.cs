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

// LoveStar
// --------------------------
// By Jeremy Craig
// Created : 7/10/2013
// Last Modified : 7/10/2013
// --------------------------

namespace LoveStar
{
    public enum Game_Window_State
    {
        Launch,
        Game
    }
    public enum Game_Draw_State
    {
        Launch,
        Game
    }


    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentManager content;
        BasicEffect basicEffect;

        LaunchManager launchManager;
        GameManager gameManager;

        Tools.KeyPress_Manager KeyPressManager;
        Tools.KeyPress KeyPress;

        Game_Window_State gameWindowState;
        Game_Draw_State gameDrawState;
        Window_Return_Info windowReturnInfo;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services, "Content");

            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 460;


            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
               (0, GraphicsDevice.Viewport.Width,     // left, right
                GraphicsDevice.Viewport.Height, 0,    // bottom, top
                0, 1);                                       // near, far plane

            IsMouseVisible = true;
            windowReturnInfo.windowTransition = false;
            gameWindowState = Game_Window_State.Launch;
            gameDrawState = Game_Draw_State.Launch;
        }

        protected override void Initialize()
        {
            KeyPressManager = new Tools.KeyPress_Manager();
            KeyPress = new Tools.KeyPress();
            launchManager = new LaunchManager(this, graphics);
            gameManager = new GameManager(this, graphics);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            launchManager.LoadContent(Services, content);
            gameManager.LoadContent(Services, content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyPress = KeyPressManager.HandleInput();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            windowReturnInfo = StateUpdate(gameTime);

            base.Update(gameTime);
        }

        protected Window_Return_Info StateUpdate(GameTime gameTime)
        {
            switch (gameWindowState)
            {
                case Game_Window_State.Launch:
                    windowReturnInfo = launchManager.Update(gameTime, KeyPress, windowReturnInfo);
                    gameDrawState = Game_Draw_State.Launch;
                    break;

                case Game_Window_State.Game:
                    windowReturnInfo = gameManager.Update(gameTime, KeyPress, windowReturnInfo);
                    gameDrawState = Game_Draw_State.Game;
                    break;
            }
            return windowReturnInfo;
        }

        public void Game_State_Handler(Window_Return_Info windowReturnInfo)
        {
            if (windowReturnInfo.windowTransition == true)
            {
                gameWindowState = windowReturnInfo.newState;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                null, null, null, null, Tools.Camera.GetMatrix());
            

            switch (gameDrawState)
            {
                case Game_Draw_State.Launch:
                    launchManager.Draw(gameTime, spriteBatch);
                    break;

                case Game_Draw_State.Game:
                    gameManager.Draw(gameTime, spriteBatch);
                    basicEffect.CurrentTechnique.Passes[0].Apply();
                    break;
            }
            
            

            spriteBatch.End();
            base.Draw(gameTime);

            Game_State_Handler(windowReturnInfo);
        }
    }

    public struct Window_Return_Info
    {
        public bool windowTransition;
        public Game_Window_State newState;
    }
}
