using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LoveStar_Point
{
    public struct KeyPress
    {
        public bool is_GamePad;
        public int key_P;
        public int key_X;
        public int key_Z;
        public int key_Up;
        public int key_Down;
        public int key_Left;
        public int key_Right;


        public KeyPress(bool is_GamePad, int key_P, int key_X, int key_Z, int key_Up, int key_Down, int key_Left, int key_Right)
        {
            this.is_GamePad = is_GamePad;
            this.key_P = key_P;
            this.key_X = key_X;
            this.key_Z = key_Z;
            this.key_Up = key_Up;
            this.key_Down = key_Down;
            this.key_Left = key_Left;
            this.key_Right = key_Right;
        }
    }



    public class KeyPress_Manager
    {
        private KeyPress StoreInput = new KeyPress(false, 0, 0, 0, 0, 0, 0, 0);


        public KeyPress HandleInput()
        {
            KeyPress keyPress = new KeyPress(StoreInput.is_GamePad, 0, 0, 0, 0, 0, 0, 0);

            // If keyboard
            if (Keyboard.GetState().IsKeyDown(Keys.P) ||
                Keyboard.GetState().IsKeyDown(Keys.X) ||
                Keyboard.GetState().IsKeyDown(Keys.Z) ||
                Keyboard.GetState().IsKeyDown(Keys.Up) ||
                Keyboard.GetState().IsKeyDown(Keys.Down) ||
                Keyboard.GetState().IsKeyDown(Keys.Left) ||
                Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                keyPress.is_GamePad = false;
            }



            // Key X
            if (Keyboard.GetState().IsKeyDown(Keys.X))
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

            // Key Up
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
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
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
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
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
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
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
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

            // Key P, Start
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                if (StoreInput.key_P > 0)
                {
                    keyPress.key_P = 2;
                }
                else
                {
                    keyPress.key_P = 1;
                }
            }

            StoreInput = keyPress;

            return keyPress;
        }
    }
}

