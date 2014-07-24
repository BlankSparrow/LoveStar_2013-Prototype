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

namespace LoveStar.Credits
{
    class Credit_Menu 
    {
        ContentManager content;

        // Variables
        private Vector2 game_Window_Size;


        public ContentManager Content
        {
            get { return content; }
        }

        public Credit_Menu(GraphicsDeviceManager graphics)
        {
            game_Window_Size = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public void LoadContent(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");

            //audio.Initialize();
        }

        public void Dispose()
        {
            Content.Unload();
        }

        public Window_Return_Info Update(GameTime gameTime, KeyPress keyPress)
        {
            Window_Return_Info window_Return_Info;
            window_Return_Info.windowTransition = false;
            window_Return_Info.newState = Game_Window_State.Credits_State;
            Base_Components.Camera.offset = Vector2.Zero;

            // Code Here

            return window_Return_Info;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
