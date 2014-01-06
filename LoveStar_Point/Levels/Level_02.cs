using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LoveStar_Point
{
    class Level_02 : Level
    {
        public Level_02(Game game, ContentManager content)
            : base(game, content, "Level_02")
        {
        }

        public override void Update(GameTime gameTime, KeyPress keyPress) 
        {
            if (keyPress.key_X == 1)
            {
                base.levelChange.set(0);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
