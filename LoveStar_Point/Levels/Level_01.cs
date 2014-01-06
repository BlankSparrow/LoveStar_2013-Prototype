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
    class Level_01 : Level
    {
        public Level_01(Game game, ContentManager content)
            : base(game, content, "Level_01")
        {
        }

        public override void Update(GameTime gameTime, KeyPress keyPress)
        {
            if (keyPress.key_X == 1)
            {
                base.levelChange.set(1);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
