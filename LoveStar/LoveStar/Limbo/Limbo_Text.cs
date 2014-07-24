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
namespace LoveStar.Limbo
{
    public partial class Limbo
    {
        private void Update_Text(KeyPress keyPress)
        {
            if (text_Page == 0)
            {
                if (keyPress.is_GamePad == false)
                    text_1 = "Welcome to Love*";
                    text_2 = "press space to continue";
                if (keyPress.is_GamePad == true)
                {
                    text_1 = "Welcome to Love*";
                    text_2 = "press A to continue";
                }
            }
            
            else if (text_Page == 1)
            {
                if (keyPress.is_GamePad == false)
                    text_1 = "Lets start with some basics";
                    text_2 = "the left & right keys are used to move";
                if (keyPress.is_GamePad == true)
                {
                    text_1 = "Lets start with some basics";
                    text_2 = "moving the left joystick will make you move";
                }
            }

            else if (text_Page == 2)
            {
                if (keyPress.is_GamePad == false)
                    text_1 = "To jump you can press";
                    text_2 = "space or Z";
                if (keyPress.is_GamePad == true)
                {
                    text_1 = "To jump";
                    text_2 = "press A";
                }
            }

            else if (text_Page == 3)
            {
                if (keyPress.is_GamePad == false)
                    text_1 = "To run";
                    text_2 = "hold down shift";
                if (keyPress.is_GamePad == true)
                {
                    text_1 = "To run";
                    text_2 = "hold down a trigger";
                }
            }

            else if (text_Page == 4)
            {
                if (keyPress.is_GamePad == false)
                    text_1 = "To crouch";
                    text_2 = "press C";
                if (keyPress.is_GamePad == true)
                {
                    text_1 = "To crouch";
                    text_2 = "press Y";
                }
            }

            else if (text_Page == 5)
            {
                if (keyPress.is_GamePad == false)
                    text_1 = "And to interact";
                    text_2 = "press X";
                if (keyPress.is_GamePad == true)
                {
                    text_1 = "And to interact";
                    text_2 = "press X";
                }
            }

            else if (text_Page == 420)
            {
                text_1 = "Skipping";
                text_2 = "";
            }
        }
    }
}
