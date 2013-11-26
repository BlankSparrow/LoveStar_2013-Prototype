using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LoveStar.LoveStar
{
    enum EntityName
    {
        Empty,
        LevelExits,
    }
    class Entity
    {
        ContentManager content;
        EntityName enName;
        private bool isUsed = false;

        public EntityName getEntityName()
        {
            return enName;
        }

        public bool IsUsed()
        {
            return this.isUsed;
        }

        public Entity(EntityName enName, Game game, GraphicsDeviceManager graphics)
        {
            this.enName = enName;
            if (enName != EntityName.Empty)
            {
                isUsed = true;
            }
        }

        public void LoadContent(IServiceProvider serviceProvider, ContentManager content)
        {
            this.content = content;
        }

        public Entity Update(GameTime gameTime, Tools.KeyPress keyPress, Player Player, Level level)
        {
            if (enName == EntityName.LevelExits)
            {
                if (level.getLevelNumber() == 1)
                {
                    if (Player.Position.X < 0)
                    {
                        level.changeLevelTo(3);
                    }
                }
                if (level.getLevelNumber() == 3)
                {
                    if (Player.Position.X > level.getLevelSize().X)
                    {
                        level.changeLevelTo(1);
                    }
                }
            }
            return this;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}
