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
    public struct LevelChange
    {
        private bool changeLevel;
        private int changeTo;

        public void set(int levelNo)
        {
            this.changeLevel = true;
            this.changeTo = levelNo;
        }

        public bool isTrue()
        {
            return changeLevel;
        }

        public int get()
        {
            this.changeLevel = false;
            return changeTo;
        }
    }
    class Level : Microsoft.Xna.Framework.DrawableGameComponent
    {
        ContentManager content;
        Game game;
        internal LevelChange levelChange;
        internal string level_Title;
        internal Texture2D level_Background;

        public Level(Game game, ContentManager content, string level_Title)
            : base(game)
        {
            this.content = content;
            this.level_Title = level_Title;
            this.level_Background = content.Load<Texture2D>("Level/" + level_Title);
            
        }

        public virtual void Update(GameTime gameTime, KeyPress keyPress)
        {
            base.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(level_Background, Vector2.Zero, Color.White);
        }
    }
}
