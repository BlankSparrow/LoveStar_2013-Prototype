// A choose your own adventure which is presented in the form of an adventure side-scroller
// Author: William Minish, Jeremy Craig
// Date Created: 25.11.12
// Date last modified: 03.02.13

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LoveStar
{
    public enum Game_Window_State
    {
        Splash_Screen_State,
        Main_Menu_State,
        Game_Mode_State,
        Limbo_State,
        Credits_State,
        Exit_State,
        
        //Secrets
        Konami_State,
    }

    public partial class Game_Base : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Base_Components.Audio audio;

        Game_Window_State game_Window_State = Game_Window_State.Game_Mode_State;//Should be// Game_Window_State.Splash_Screen_State;

        Splash_Screen.Splash_Screen splash_Screen;
        Main_Menu.Main_Menu main_Menu;
        Game_Components.Game_Mode game_Mode;
        Limbo.Limbo limbo;
        Credits.Credit_Menu credit_Menu;

        // Secrets
        Secrets.Konami konami;


        // Varibles

        private Color Screen_Color = Color.Black;

        public Game_Base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Screen_Handler();

        }

        protected void Screen_Handler()
        {
            // Application Window Size
            int window_Width = 1000; // 900; 
            int window_Height = 650; // 600; 

            graphics.IsFullScreen = false;
            IsMouseVisible = true;

            if (graphics.IsFullScreen == false)
            {
                graphics.PreferredBackBufferWidth = window_Width;
                graphics.PreferredBackBufferHeight = window_Height;
            }
            else
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
        }

        protected override void Initialize()
        {
            audio = new Base_Components.Audio();
            audio.Initialize();

            splash_Screen = new Splash_Screen.Splash_Screen(this, graphics);
            main_Menu = new Main_Menu.Main_Menu(this, graphics);
            game_Mode = new Game_Components.Game_Mode(graphics);
            limbo = new Limbo.Limbo(graphics);
            credit_Menu = new Credits.Credit_Menu(graphics);

            // Secrets
            konami = new Secrets.Konami(graphics);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            splash_Screen.LoadContent(Services);
            main_Menu.LoadContent(Services);
            game_Mode.LoadContent(Services);
            limbo.LoadContent(Services);
            credit_Menu.LoadContent(Services);

            // Secrets
            konami.LoadContent(Services);


        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyPress keyPress;
            keyPress = HandleInput();

            Window_Return_Info window_Return_Info;
            window_Return_Info.windowTransition = false;
            window_Return_Info.newState = Game_Window_State.Splash_Screen_State;

            switch (game_Window_State)
            {
                case Game_Window_State.Splash_Screen_State:
                    Screen_Color = Color.Black;
                    window_Return_Info = splash_Screen.Update(gameTime, keyPress);
                    break;

                case Game_Window_State.Main_Menu_State:
                    Screen_Color = Color.White;
                    window_Return_Info = main_Menu.Update(gameTime, keyPress);
                    break;

                case Game_Window_State.Game_Mode_State:
                    Screen_Color = Color.Black;
                    window_Return_Info = game_Mode.Update(gameTime, keyPress);
                    break;

                case Game_Window_State.Limbo_State:
                    Screen_Color = Color.Black;
                    window_Return_Info = limbo.Update(gameTime, keyPress);
                    break;

                case Game_Window_State.Credits_State:
                    Screen_Color = Color.White;
                    window_Return_Info = credit_Menu.Update(gameTime, keyPress);
                    break;

                case Game_Window_State.Konami_State:
                    Screen_Color = Color.Black;
                    window_Return_Info = konami.Update(gameTime, keyPress);
                    break;

                case Game_Window_State.Exit_State:
                    Exit();
                    break;
            }

            Game_State_Handler(window_Return_Info);
            audio.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Screen_Color);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                null, null, null, null, Base_Components.Camera.GetMatrix());

            State_Draw(gameTime, spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void State_Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (game_Window_State)
            {
                case Game_Window_State.Splash_Screen_State:

                    Screen_Color = Color.Black;
                    splash_Screen.Draw(gameTime, spriteBatch);
                    break;

                case Game_Window_State.Main_Menu_State:
                    Screen_Color = Color.White;
                    main_Menu.Draw(gameTime, spriteBatch);
                    break;

                case Game_Window_State.Game_Mode_State:
                    Screen_Color = Color.Black;
                    game_Mode.Draw(gameTime, spriteBatch);
                    break;

                case Game_Window_State.Limbo_State:
                    Screen_Color = Color.Black;
                    limbo.Draw(gameTime, spriteBatch);
                    break;

                case Game_Window_State.Credits_State:
                    Screen_Color = Color.White;
                    credit_Menu.Draw(gameTime, spriteBatch);
                    break;

                // Secret

                case Game_Window_State.Konami_State:
                    Screen_Color = Color.Black;
                    konami.Draw(gameTime, spriteBatch);
                    break;
            }
        }
    }
}
