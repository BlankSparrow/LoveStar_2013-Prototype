using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LoveStar
{
    public struct KeyPress
    {
        public bool is_GamePad;
        public int key_Esc;
        public int key_Space;
        public int key_X;
        public int key_Z;
        public int key_A;
        public int key_B;
        public int key_C;
        public int key_T;
        public int key_Shift;
        public int key_Up;
        public int key_Down;
        public int key_Left;
        public int key_Right;
        public int key_Ctrl;

        public KeyPress(bool is_GamePad, int key_Esc, int key_Space, int key_X, int key_Z, int key_A, int key_B, int key_C, int key_T, int key_Shift, int key_Up, int key_Down, int key_Left, int key_Right, int key_Ctrl)
        {
            this.is_GamePad = is_GamePad;
            this.key_Esc = key_Esc;
            this.key_Space = key_Space;
            this.key_X = key_X;
            this.key_Z = key_Z;
            this.key_A = key_A;
            this.key_B = key_B;
            this.key_C = key_C;
            this.key_T = key_T;
            this.key_Shift = key_Shift;
            this.key_Up = key_Up;
            this.key_Down = key_Down;
            this.key_Left = key_Left;
            this.key_Right = key_Right;
            this.key_Ctrl = key_Ctrl;
        }
    }

    public struct Window_Return_Info
    {
        public bool windowTransition;
        public Game_Window_State newState;
    }

    public partial class Game_Base
    {
        private KeyPress StoreInput = new KeyPress(false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);


        private KeyPress HandleInput()
        {
            KeyPress keyPress = new KeyPress(StoreInput.is_GamePad, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);


            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);

            // Exit the game when back is pressed.
            if (gamepadState.Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Exit();
            }
            
            
            // If keyboard
            if (Keyboard.GetState().IsKeyDown(Keys.Escape ) || Keyboard.GetState().IsKeyDown(Keys.Space ) ||Keyboard.GetState().IsKeyDown(Keys.X ) ||
                Keyboard.GetState().IsKeyDown(Keys.Z ) ||Keyboard.GetState().IsKeyDown(Keys.A ) ||Keyboard.GetState().IsKeyDown(Keys.B ) ||
                Keyboard.GetState().IsKeyDown(Keys.C ) ||Keyboard.GetState().IsKeyDown(Keys.RightShift ) ||Keyboard.GetState().IsKeyDown(Keys.LeftShift ) ||
                Keyboard.GetState().IsKeyDown(Keys.Up ) ||Keyboard.GetState().IsKeyDown(Keys.Down ) ||Keyboard.GetState().IsKeyDown(Keys.Left ) ||
                Keyboard.GetState().IsKeyDown(Keys.Right ) ||Keyboard.GetState().IsKeyDown(Keys.RightControl ) ||Keyboard.GetState().IsKeyDown(Keys.LeftControl ) ||
                Keyboard.GetState().IsKeyDown(Keys.T))
            {
                keyPress.is_GamePad = false;
            }

            // If Controller
            if (gamepadState.Buttons.X == ButtonState.Pressed || gamepadState.Buttons.A == ButtonState.Pressed || gamepadState.Buttons.B == ButtonState.Pressed ||
                gamepadState.Buttons.Y == ButtonState.Pressed || gamepadState.Buttons.Start == ButtonState.Pressed || gamepadState.DPad.Up == ButtonState.Pressed ||
                gamepadState.ThumbSticks.Left.Y >= 0.5 || gamepadState.DPad.Down == ButtonState.Pressed || gamepadState.ThumbSticks.Left.Y <= -0.5 ||
                gamepadState.DPad.Left == ButtonState.Pressed || gamepadState.ThumbSticks.Left.X <= -0.5 || gamepadState.DPad.Right == ButtonState.Pressed ||
                gamepadState.ThumbSticks.Left.X >= 0.5 || gamepadState.Buttons.RightShoulder == ButtonState.Pressed)
            {
                keyPress.is_GamePad = true;
            }


            // Key X
            if (Keyboard.GetState().IsKeyDown(Keys.X) || gamepadState.Buttons.X == ButtonState.Pressed)
            {
                if (StoreInput.key_X > 0)
                {
                    keyPress.key_X = 2;
                }

                else
                {
                    keyPress.key_X = 1;
                }
            }

            
            // Key Z
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (StoreInput.key_Z > 0)
                {
                    keyPress.key_Z = 2;
                }
                else
                {
                    keyPress.key_Z = 1;
                }
            }

            // Key A
            if (Keyboard.GetState().IsKeyDown(Keys.A) || gamepadState.Buttons.A == ButtonState.Pressed)
            {
                if (StoreInput.key_A > 0)
                {
                    keyPress.key_A = 2;
                }
                else
                {
                    keyPress.key_A = 1;
                }
            }

            // Key B
            if (Keyboard.GetState().IsKeyDown(Keys.B) || gamepadState.Buttons.B == ButtonState.Pressed)
            {
                if (StoreInput.key_B > 0)
                {
                    keyPress.key_B = 2;
                }
                else
                {
                    keyPress.key_B = 1;
                }
            }

            // Key C
            if (Keyboard.GetState().IsKeyDown(Keys.C) || gamepadState.Buttons.Y == ButtonState.Pressed)
            {
                if (StoreInput.key_C > 0)
                {
                    keyPress.key_C = 2;
                }
                else
                {
                    keyPress.key_C = 1;
                }
            }

            // Key T
            if (Keyboard.GetState().IsKeyDown(Keys.T) || gamepadState.Buttons.RightShoulder == ButtonState.Pressed)
            {
                if (StoreInput.key_T > 0)
                {
                    keyPress.key_T = 2;
                }
                else
                {
                    keyPress.key_T = 1;
                }
            }

            // Key Shift
            if (Keyboard.GetState().IsKeyDown(Keys.RightShift) || Keyboard.GetState().IsKeyDown(Keys.LeftShift) ||
                gamepadState.Triggers.Left >= 0.2 || gamepadState.Triggers.Right >= 0.2)
            {
                if (StoreInput.key_Shift > 0)
                {
                    keyPress.key_Shift = 2;
                }
                else
                {
                    keyPress.key_Shift = 1;
                }
            };

            // Key Esc
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || gamepadState.Buttons.Start == ButtonState.Pressed)
            {
                if (StoreInput.key_Esc > 0)
                {
                    keyPress.key_Esc = 2;
                }
                else
                {
                    keyPress.key_Esc = 1;
                }
            }

            // Key Up
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || gamepadState.DPad.Up == ButtonState.Pressed || gamepadState.ThumbSticks.Left.Y >= 0.5)
            {
                if (StoreInput.key_Up > 0)
                {
                    keyPress.key_Up = 2;
                }
                else
                {
                    keyPress.key_Up = 1;
                }
            }

            // Key Down
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || gamepadState.DPad.Down == ButtonState.Pressed || gamepadState.ThumbSticks.Left.Y <= -0.5)
            {
                if (StoreInput.key_Down > 0)
                {
                    keyPress.key_Down = 2;
                }
                else
                {
                    keyPress.key_Down = 1;
                }
            }

            // Key Left
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || gamepadState.DPad.Left == ButtonState.Pressed || gamepadState.ThumbSticks.Left.X <= -0.5)
            {
                if (StoreInput.key_Left > 0)
                {
                    keyPress.key_Left = 2;
                }
                else
                {
                    keyPress.key_Left = 1;
                }
            }

            // Key Right
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || gamepadState.DPad.Right == ButtonState.Pressed || gamepadState.ThumbSticks.Left.X >= 0.5)
            {
                if (StoreInput.key_Right > 0)
                {
                    keyPress.key_Right = 2;
                }
                else
                {
                    keyPress.key_Right = 1;
                }
            }

            // Key Space, Enter
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Enter) || gamepadState.Buttons.A == ButtonState.Pressed)
            {
                if (StoreInput.key_Space > 0)
                {
                    keyPress.key_Space = 2;
                }
                else
                {
                    keyPress.key_Space = 1;
                }
            }

            // Key Ctrl
            if (Keyboard.GetState().IsKeyDown(Keys.RightControl) || Keyboard.GetState().IsKeyDown(Keys.LeftControl) )
            {
                if (StoreInput.key_Ctrl > 0)
                {
                    keyPress.key_Ctrl = 2;
                }
                else
                {
                    keyPress.key_Ctrl = 1;
                }
            }

            StoreInput = keyPress;

            return keyPress;
        }

        void Game_State_Handler(Window_Return_Info window_Return_Info)
        {
            if (window_Return_Info.windowTransition)
            {
                game_Window_State = window_Return_Info.newState;
            }
        }
    }
}
