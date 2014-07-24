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
using System.IO;

namespace LoveStar.Main_Menu
{
    class Main_Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        enum Menu_State
        {
            menu_Fade_In,
            menu_Select,
            menu_Fade_Out,
            menu_Change_State,
        }

        ContentManager content;
        
        // Assets
        Texture2D Title_Text;
        Texture2D Menu_Text_1;
        Texture2D Menu_Text_2;
        Texture2D Menu_Text_3;
        Texture2D Menu_Text_1_B;
        Texture2D Menu_Text_1_G;
        Texture2D Menu_Text_2_B;
        Texture2D Menu_Text_2_G;
        Texture2D Menu_Text_3_B;
        Texture2D Menu_Text_3_G;

        Texture2D screen_Fade;

        // Variables
        Window_Return_Info next_window_state;
        private Menu_State menu_State = Menu_State.menu_Fade_In;

        const double fade_Delay = .013;
        const float fadeIncrement = 0.01f;

        private float AlphaValue;
        private double FadeDelay;

        private bool reload = true;
        private int select = 0;
        private int select_Max = 3;

        private Vector2 game_Window_Size;

        private bool menu_Color_1;
        private bool menu_Color_2;
        private bool menu_Color_3;

        public ContentManager Content
        {
            get { return content; }
        }

        public Main_Menu(Game game, GraphicsDeviceManager graphics)
            : base(game)
        {
            game_Window_Size = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public void LoadContent(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");
        }

        public void Reload()
        {
            reload = false;

            Title_Text = content.Load<Texture2D>("Text/Main_Menu/Title_Text");
            Menu_Text_1_B = content.Load<Texture2D>("Text/Main_Menu/Start_Text_Black");
            Menu_Text_1_G = content.Load<Texture2D>("Text/Main_Menu/Start_Text_Grey");
            Menu_Text_2_B = content.Load<Texture2D>("Text/Main_Menu/Credits_Text_Black");
            Menu_Text_2_G = content.Load<Texture2D>("Text/Main_Menu/Credits_Text_Grey");
            Menu_Text_3_B = content.Load<Texture2D>("Text/Main_Menu/Exit_Text_Black");
            Menu_Text_3_G = content.Load<Texture2D>("Text/Main_Menu/Exit_Text_Grey");
            Menu_Text_1 = Menu_Text_1_G;
            Menu_Text_2 = Menu_Text_2_G;
            Menu_Text_3 = Menu_Text_3_G;

            screen_Fade = content.Load<Texture2D>("Fades/Black");

            menu_State = Menu_State.menu_Fade_In;

            AlphaValue = 0f;
            FadeDelay = 0;

            select = 0;
        }

        public void Dismiss()
        {
            reload = true;
        }

        public Window_Return_Info Update(GameTime gameTime, KeyPress keyPress)
        {
            Window_Return_Info window_Return_Info;
            window_Return_Info.windowTransition = false;
            window_Return_Info.newState = Game_Window_State.Main_Menu_State;
            Base_Components.Camera.offset = Vector2.Zero;

            if (reload == true)
            {
                Reload();
            }

            Menu_Selecting(keyPress);

            switch(menu_State)
            {
                case Menu_State.menu_Fade_In:
                    
                    FadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                    if (FadeDelay <= 0)
                    {
                        FadeDelay = fade_Delay;
                        AlphaValue += fadeIncrement;

                        if (AlphaValue >= 1)
                        {
                            AlphaValue = MathHelper.Clamp(AlphaValue, 0, 1);
                            menu_State = Menu_State.menu_Select;
                            screen_Fade = content.Load<Texture2D>("Fades/White");
                        }
                    }
                    
                    break;

                case Menu_State.menu_Select:
                    
                    if (keyPress.key_Space == 1)
                    {
                        if (select == 1)
                        {
                            next_window_state.newState = Game_Window_State.Limbo_State;
                        }
                        else if (select == 2)
                        {
                            next_window_state.newState = Game_Window_State.Credits_State;
                        }
                        else if (select == 3)
                        {
                            next_window_state.newState = Game_Window_State.Exit_State;
                        }

                        menu_State = Menu_State.menu_Fade_Out;
                    }
                    
                    break;

                case Menu_State.menu_Fade_Out:

                    FadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                    if (FadeDelay <= 0)
                    {
                        FadeDelay = fade_Delay;
                        AlphaValue -= fadeIncrement;

                        if (AlphaValue <= 0)
                        {
                            AlphaValue = MathHelper.Clamp(AlphaValue, 0, 1);
                            menu_State = Menu_State.menu_Change_State;
                        }
                    }

                    break;

                case Menu_State.menu_Change_State:

                    AlphaValue = 0f;
                    Dismiss();
                    window_Return_Info.windowTransition = true;
                    window_Return_Info.newState = next_window_state.newState;

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

            Menu_Text(spriteBatch);

            spriteBatch.Draw(screen_Fade, new Rectangle(0, 0, (int)game_Window_Size.X, (int)game_Window_Size.Y), Color.Lerp(Color.White, Color.Transparent, AlphaValue));
        }

        private void Menu_Text(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Title_Text, new Rectangle((int)game_Window_Size.X / 2 - Title_Text.Width / 2, (int)game_Window_Size.Y / 2 - Title_Text.Height / 2 - 100,
                Title_Text.Width, Title_Text.Height), Color.White);

            spriteBatch.Draw(Menu_Text_1, new Rectangle((int)game_Window_Size.X / 2 - Menu_Text_1.Width / 2, (int)game_Window_Size.Y / 2 - Menu_Text_1.Height / 2,
                Menu_Text_1.Width, Menu_Text_1.Height), Color.White);

            spriteBatch.Draw(Menu_Text_2, new Rectangle((int)game_Window_Size.X / 2 - Menu_Text_2.Width / 2, (int)game_Window_Size.Y / 2 - Menu_Text_2.Height / 2 + 70,
                Menu_Text_2.Width, Menu_Text_2.Height), Color.White);

            spriteBatch.Draw(Menu_Text_3, new Rectangle((int)game_Window_Size.X / 2 - Menu_Text_3.Width / 2, (int)game_Window_Size.Y / 2 - Menu_Text_3.Height / 2 + 140,
                Menu_Text_3.Width, Menu_Text_3.Height), Color.White);
        }

        private void Menu_Selecting(KeyPress keyPress)
        {
            if (keyPress.key_Esc == 1)
            {
                select = select_Max;
            }

            if (keyPress.key_Up > 0 && keyPress.key_Down > 0)
            {
                select = 0;
            }

            else if (keyPress.key_Up == 1)
            {
                if (select == 0)
                {
                    select = 1;
                }
                else if (select <= 1)
                {
                    select = 1;
                }
                else
                {
                    select -= 1;
                }
            }

            else if (keyPress.key_Down == 1)
            {
                if (select == 0)
                {
                    select = select_Max;
                }
                else if (select >= select_Max)
                {
                    select = select_Max;
                }
                else
                {
                    select += 1;
                }
            }


            if (select == 0)
            {
                Menu_Text_1 = Menu_Text_1_G;
                Menu_Text_2 = Menu_Text_2_G;
                Menu_Text_3 = Menu_Text_3_G;
            }
            else if (select == 1)
            {
                Menu_Text_1 = Menu_Text_1_B;
                Menu_Text_2 = Menu_Text_2_G;
                Menu_Text_3 = Menu_Text_3_G;
            }
            else if (select == 2)
            {
                Menu_Text_1 = Menu_Text_1_G;
                Menu_Text_2 = Menu_Text_2_B;
                Menu_Text_3 = Menu_Text_3_G;
            }
            else if (select == 3)
            {
                Menu_Text_1 = Menu_Text_1_G;
                Menu_Text_2 = Menu_Text_2_G;
                Menu_Text_3 = Menu_Text_3_B;
            }
        }
    }
}
