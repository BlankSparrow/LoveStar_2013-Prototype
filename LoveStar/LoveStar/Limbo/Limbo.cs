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

namespace LoveStar.Limbo
{
    public partial class Limbo 
    {
        enum Limbo_State
        {
            fade_In,
            text,
            fade_Out,
            change_state,
        }

        KeyPress sub_KeyPress;
        ContentManager content;

        // Assets

        Texture2D screen_Fade;
        SpriteFont Limbo_Font;


        // Variables

        private bool reload = true;
        private Limbo_State limbo_State = Limbo_State.fade_In;

        private Vector2 game_Window_Size;

        const double fade_Delay = .013;
        const float fadeIncrement = 0.02f;

        private float AlphaValue;
        private double FadeDelay;

        private int text_Page;
        private string text_1;
        private string text_2;


        public ContentManager Content
        {
            get { return content; }
        }

        public Limbo(GraphicsDeviceManager graphics)
        {
            game_Window_Size = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public void LoadContent(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");
        }

        private void Reload(KeyPress keypress)
        {
            limbo_State = Limbo_State.fade_In;

            screen_Fade = content.Load<Texture2D>("Fades/White");
            Limbo_Font = content.Load<SpriteFont>("Fonts/Limbo_Font");

            AlphaValue = 0f;
            FadeDelay = 0;

            sub_KeyPress = keypress;
            text_Page = 0;
            Update_Text(keypress);

            reload = false;
        }

        public void Dismiss()
        {
            reload = true;
            Content.Unload();
        }

        public Window_Return_Info Update(GameTime gameTime, KeyPress keyPress)
        {
            Window_Return_Info window_Return_Info;
            window_Return_Info.windowTransition = false;
            window_Return_Info.newState = Game_Window_State.Game_Mode_State;
            Base_Components.Camera.offset = Vector2.Zero;

            if (reload == true)
            {
                Reload(keyPress);
            }

            if (keyPress.key_Esc == 1)
            {
                limbo_State = Limbo_State.fade_Out;
                screen_Fade = content.Load<Texture2D>("Fades/Black");
                text_Page = 420;
            }

            Update_Text(keyPress);

            switch (limbo_State)
            {
                case Limbo_State.fade_In:
                    
                    FadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                    if (FadeDelay <= 0)
                    {
                        FadeDelay = fade_Delay;
                        AlphaValue += fadeIncrement;

                        if (AlphaValue >= 1)
                        {
                            AlphaValue = MathHelper.Clamp(AlphaValue, 0, 1);
                            limbo_State = Limbo_State.text;
                            screen_Fade = content.Load<Texture2D>("Fades/Black");
                        }
                    }
                    break;

                case Limbo_State.text:
                    if (keyPress.key_Space == 1)
                    {
                        limbo_State = Limbo_State.fade_Out;
                    }

                    break;

                case Limbo_State.fade_Out:
                    FadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                    if (FadeDelay <= 0)
                    {
                        FadeDelay = fade_Delay;
                        AlphaValue -= fadeIncrement;

                        if (AlphaValue <= 0)
                        {
                            AlphaValue = MathHelper.Clamp(AlphaValue, 0, 1);
                            if (text_Page >= 5)
                            {
                                limbo_State = Limbo_State.change_state;
                            }
                            else
                            {
                                limbo_State = Limbo_State.fade_In;
                                text_Page += 1;
                            }
                        }
                    }
                    break;

                case Limbo_State.change_state:
                    Dismiss();
                    window_Return_Info.windowTransition = true;
                    window_Return_Info.newState = Game_Window_State.Game_Mode_State;
                    break;
            }
            return window_Return_Info;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (reload == true)
            {
                Reload(sub_KeyPress);
            }

            Draw_Text(spriteBatch);

            spriteBatch.Draw(screen_Fade, new Rectangle(0,0, (int)game_Window_Size.X, (int)game_Window_Size.Y),Color.Lerp(Color.White, Color.Transparent, AlphaValue));
        }

        public void Draw_Text(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Limbo_Font, text_1, new Vector2(((int)game_Window_Size.X / 2) - (Limbo_Font.MeasureString(text_1).X / 2),
                ((int)game_Window_Size.Y / 2) - (Limbo_Font.MeasureString(text_1).Y / 2)), Color.White);
            spriteBatch.DrawString(Limbo_Font, text_2, new Vector2(((int)game_Window_Size.X / 2) - (Limbo_Font.MeasureString(text_2).X / 2),
                ((int)game_Window_Size.Y / 2) + (Limbo_Font.MeasureString(text_2).Y / 2)), Color.White);
        }
    }
}
