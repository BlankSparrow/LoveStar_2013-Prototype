using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LoveStar.Splash_Screen
{
    public class Splash_Screen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        enum Splash
        {
            nwp_Logo,
            change_State,
        }

        ContentManager content;

        // Assets

        Texture2D nwp_Logo;

        // Variables
        private bool reload = true;
        private double elapsedTime;
        private Vector2 game_Window_Size;

        private Splash splash_state = Splash.nwp_Logo;

        const double fade_Delay = .013;
        const float fadeIncrement = 0.01f;
        const double splash_Pause = 2;

        private int nwp_logo_fade;
        private float nwp_AlphaValue;
        private double nwp_FadeDelay;

        private int konami;


        private void Reload()
        {
            splash_state = Splash.nwp_Logo;

            nwp_Logo = content.Load<Texture2D>("Logo/nwp_white");

            nwp_logo_fade = 0;
            nwp_AlphaValue = 1f;
            nwp_FadeDelay = 1;

            konami = 0;

            reload = false;
        }

        private void Dismiss()
        {
            reload = true;
            content.Unload();
        }

        public Splash_Screen(Game game, GraphicsDeviceManager graphics)
            : base(game)
        {
            game_Window_Size = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public void LoadContent(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");
        }

        public Window_Return_Info Update(GameTime gameTime, KeyPress keyPress)
        {
            Window_Return_Info window_Return_Info;
            window_Return_Info.windowTransition = false;
            window_Return_Info.newState = Game_Window_State.Splash_Screen_State;
            Base_Components.Camera.offset = Vector2.Zero;

            if (reload == true)
            {
                Reload();
            }


            if (keyPress.key_Up == 1 && konami == 0)
            {
                konami += 1;
            }
            else if (keyPress.key_Up == 1 && konami == 1)
            {
                konami += 1;
            }
            else if (keyPress.key_Down == 1 && konami == 2)
            {
                konami += 1;
            }
            else if (keyPress.key_Down == 1 && konami == 3)
            {
                konami += 1;
            }
            else if (keyPress.key_Left == 1 && konami == 4)
            {
                konami += 1;
            }
            else if (keyPress.key_Right == 1 && konami == 5)
            {
                konami += 1;
            }
            else if (keyPress.key_Left == 1 && konami == 6)
            {
                konami += 1;
            }
            else if (keyPress.key_Right == 1 && konami == 7)
            {
                konami += 1;
            }
            else if (keyPress.key_A == 1 && konami == 8)
            {
                konami += 1;
            }
            else if (keyPress.key_B == 1 && konami == 9)
            {
                window_Return_Info.windowTransition = true;
                window_Return_Info.newState = Game_Window_State.Konami_State;
                Dismiss();
            }

            switch(splash_state)
            {
                case Splash.nwp_Logo:

                    // nwp_logo fade in
                    if (nwp_logo_fade == 0)
                    {
                        nwp_FadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                        if (nwp_FadeDelay <= 0)
                        {
                            nwp_FadeDelay = fade_Delay;
                            nwp_AlphaValue -= fadeIncrement;

                            if (nwp_AlphaValue <= 0)
                            {
                                nwp_AlphaValue = MathHelper.Clamp(nwp_AlphaValue, 0, 1);
                                nwp_logo_fade += 1;
                            }
                        }
                    }
                    
                    // nwp_logo pause
                    else if (nwp_logo_fade == 1)
                    {
                        if (elapsedTime >= splash_Pause)
                        {
                            nwp_logo_fade += 1;
                            nwp_AlphaValue = 0f;
                        }
                        else
                        {
                            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }

                    // nwp_logo fade out
                    else if (nwp_logo_fade == 2)
                    {
                        nwp_FadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                        if (nwp_FadeDelay <= 0)
                        {
                            nwp_FadeDelay = fade_Delay;
                            nwp_AlphaValue += fadeIncrement;

                            if (nwp_AlphaValue >= 1)
                            {
                                nwp_AlphaValue = MathHelper.Clamp(nwp_AlphaValue, 0, 1);
                                splash_state = Splash.change_State;
                            }
                        }
                    }
                    break;



                case Splash.change_State:
                    
                    // Change Game_Window_State

                    window_Return_Info.windowTransition = true;
                    window_Return_Info.newState = Game_Window_State.Main_Menu_State;
                    Dismiss();
                    break;
            }
            return window_Return_Info;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (reload == true)
            {
                Reload();
            }

            switch (splash_state)
            {
                case Splash.nwp_Logo:
                    spriteBatch.Draw(nwp_Logo, new Rectangle(
                        (int)game_Window_Size.X / 2 - nwp_Logo.Width / 2, (int)game_Window_Size.Y / 2 - nwp_Logo.Height / 2,
                        nwp_Logo.Width, nwp_Logo.Height),
                        Color.Lerp(Color.White, Color.Transparent, nwp_AlphaValue));
                    break;

                case Splash.change_State:
                    break;
            }
        }
    }
}

