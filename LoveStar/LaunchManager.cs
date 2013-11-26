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
    class LaunchManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public LaunchManager(Game game, GraphicsDeviceManager graphics)
            : base(game)
        {
        }

        public void LoadContent(IServiceProvider serviceProvider, ContentManager content)
        {
        }

        public Window_Return_Info Update(GameTime gameTime, Tools.KeyPress keyPress, Window_Return_Info window_Return_Info)
        {
            window_Return_Info.windowTransition = false;
            window_Return_Info.newState = Game_Window_State.Game;
            Tools.Camera.offset = Vector2.Zero;

            window_Return_Info.windowTransition = true;

            return window_Return_Info;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}
