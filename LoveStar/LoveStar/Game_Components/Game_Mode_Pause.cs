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

namespace LoveStar.Game_Components
{
    public partial class Game_Mode
    {
        // Assets
        Texture2D pause_Background;
        Texture2D pause_Fade;

        Texture2D Pause_Title_Text;
        Texture2D Pause_Menu_Text_1;
        Texture2D Pause_Menu_Text_2;
        Texture2D Pause_Menu_Text_1_B;
        Texture2D Pause_Menu_Text_1_G;
        Texture2D Pause_Menu_Text_2_B;
        Texture2D Pause_Menu_Text_2_G;

        // Varibles

        private int select = 0;
        private int select_Max = 2;

        private void Pause_Reload()
        {
            pause_Background = content.Load<Texture2D>("Pause_Menu/Pause_Background");
            pause_Fade = content.Load<Texture2D>("Fades/Black");

            Pause_Title_Text = content.Load<Texture2D>("Text/Pause_Menu/Title_Text");
            Pause_Menu_Text_1_B = content.Load<Texture2D>("Text/Pause_Menu/Resume_Text_B");
            Pause_Menu_Text_1_G = content.Load<Texture2D>("Text/Pause_Menu/Resume_Text_G");
            Pause_Menu_Text_2_B = content.Load<Texture2D>("Text/Pause_Menu/Leave_Text_B");
            Pause_Menu_Text_2_G = content.Load<Texture2D>("Text/Pause_Menu/Leave_Text_G");

            Pause_Menu_Text_1 = Pause_Menu_Text_1_G;
            Pause_Menu_Text_2 = Pause_Menu_Text_2_G;

            select = 0;
        }


        private void Pause_Update(KeyPress keyPress)
        {
            Menu_Selecting(keyPress);

            if (keyPress.key_Space == 1)
            {
                if (select == 1)
                {
                    playing_State = Playing_State.game_state; 
                    is_paused = false;
                    player.isOnGround = false;
                }
                else if (select == 2)
                {
                    playing_State = Playing_State.fade_out_state;
                    next_Window_State_Bool = true;
                }

            }
        }

        private void Pause_Draw(SpriteBatch spriteBatch)
        {
            Pause_Draw_Base(spriteBatch);
            Pause_Draw_Title(spriteBatch);
            Pause_Draw_Menu(spriteBatch);
        }

        private void Pause_Draw_Base(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pause_Fade, new Rectangle((int)Base_Components.Camera.offset.X - 10, (int)Base_Components.Camera.offset.Y - 10, (int)game_Window_Size.X + 20, (int)game_Window_Size.Y + 20), Color.Lerp(Color.White, Color.Transparent, 0.5f));

            spriteBatch.Draw(pause_Background, new Rectangle(
                ((int)Base_Components.Camera.offset.X + ((int)game_Window_Size.X / 2) - (pause_Background.Width / 2)),
                ((int)Base_Components.Camera.offset.Y + ((int)game_Window_Size.Y / 2) - (pause_Background.Height / 2)),
                pause_Background.Width, pause_Background.Height), Color.White);
        }

        private void Pause_Draw_Title(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Pause_Title_Text, new Rectangle((int)Base_Components.Camera.offset.X + ((int)game_Window_Size.X / 2) - (Pause_Title_Text.Width / 2),
                                                            (int)Base_Components.Camera.offset.Y + ((int)game_Window_Size.Y / 2) - (Pause_Title_Text.Height / 2) - 45,
                                                            Pause_Title_Text.Width, Pause_Title_Text.Height), Color.White);
        }

        private void Pause_Draw_Menu(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Pause_Menu_Text_1, new Rectangle((int)Base_Components.Camera.offset.X + ((int)game_Window_Size.X / 2) - (Pause_Menu_Text_1.Width / 2),
                                                (int)Base_Components.Camera.offset.Y + ((int)game_Window_Size.Y / 2) - (Pause_Menu_Text_1.Height / 2) + 15,
                                                Pause_Menu_Text_1.Width, Pause_Menu_Text_1.Height), Color.White);

            spriteBatch.Draw(Pause_Menu_Text_2, new Rectangle((int)Base_Components.Camera.offset.X + ((int)game_Window_Size.X / 2) - (Pause_Menu_Text_2.Width / 2),
                                    (int)Base_Components.Camera.offset.Y + ((int)game_Window_Size.Y / 2) - (Pause_Menu_Text_2.Height / 2) + 55,
                                    Pause_Menu_Text_2.Width, Pause_Menu_Text_2.Height), Color.White);
        }

        private void Menu_Selecting(KeyPress keyPress)
        {
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
                Pause_Menu_Text_1 = Pause_Menu_Text_1_G;
                Pause_Menu_Text_2 = Pause_Menu_Text_2_G;
            }
            else if (select == 1)
            {
                Pause_Menu_Text_1 = Pause_Menu_Text_1_B;
                Pause_Menu_Text_2 = Pause_Menu_Text_2_G;
            }
            else if (select == 2)
            {
                Pause_Menu_Text_1 = Pause_Menu_Text_1_G;
                Pause_Menu_Text_2 = Pause_Menu_Text_2_B;
            }
        }
    }
}
