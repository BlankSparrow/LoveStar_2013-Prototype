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
    class GameManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        LoveStar.Level level;
        LoveStar.Player player;
        LoveStar.Entity[] Entities;
        //LoveStar.Element element;
        //LoveStar.CutScenes cutScenes;

        //bool isCutScene;
        bool isPause;

        public GameManager(Game game, GraphicsDeviceManager graphics)
            : base(game)
        {
            level = new LoveStar.Level(game, graphics);
            player = new LoveStar.Player(game, graphics);
            Entities = new LoveStar.Entity[12];
            Entities[0] = new LoveStar.Entity(LoveStar.EntityName.LevelExits, game, graphics);
            Entities[1] = new LoveStar.Entity(LoveStar.EntityName.Empty, game, graphics);
        }

        public void LoadContent(IServiceProvider serviceProvider, ContentManager content)
        {
            level.LoadContent(serviceProvider, content);
            player.LoadContent(serviceProvider, content);
            Entities[0].LoadContent(serviceProvider, content);
        }

        public Window_Return_Info Update(GameTime gameTime, Tools.KeyPress keyPress, Window_Return_Info window_Return_Info)
        {
            window_Return_Info.windowTransition = false;
            window_Return_Info.newState = Game_Window_State.Game;

            level = level.Update(gameTime, keyPress, level);
            if (level.shouldPlayerShift() == true)
            {
                player.getScarf().FreezeScarf(player.Position);
                player.Position = level.getPlayerStartPosition();
                player.getScarf().UnfreezeScarf(player.Position);
                Tools.Camera.offset.X = player.Position.X - (GraphicsDevice.Viewport.Width / 2);
            }
            player = player.Update(gameTime, keyPress, player);

            for (int i = 0; i >= 0; i++)
            {
                if (Entities[i].IsUsed() != true)
                {
                    break;
                }
                else
                {
                    Entities[i].Update(gameTime, keyPress, player, level);
                }
            }

            CameraPosition(level, player);

            return window_Return_Info;
        }

        public void CameraPosition(LoveStar.Level level, LoveStar.Player player){
            if (player.Position.X <= (GraphicsDevice.Viewport.Width / 2))
            {
                if (Tools.Camera.offset.X < 0)
                {
                    Tools.Camera.offset.X = 0;
                }
                else
                {
                    Tools.Camera.offset.X = MathHelper.Lerp(Tools.Camera.offset.X,
                        0, 0.04f);
                }
            }
            else if (player.Position.X >= (level.getLevelSize().X - (GraphicsDevice.Viewport.Width / 2)))
            {
                if (Tools.Camera.offset.X > (level.getLevelSize().X - GraphicsDevice.Viewport.Width))
                {
                    Tools.Camera.offset.X = (level.getLevelSize().X - GraphicsDevice.Viewport.Width);
                }
                else
                {
                    Tools.Camera.offset.X = MathHelper.Lerp(Tools.Camera.offset.X,
                        level.getLevelSize().X - GraphicsDevice.Viewport.Width, 0.04f);
                }
                    
            }
            else
            {
                Tools.Camera.offset.X = MathHelper.Lerp(Tools.Camera.offset.X, player.Position.X
                    - (GraphicsDevice.Viewport.Width / 2), 0.04f);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            level.Draw(gameTime, spriteBatch);
            for (int i = 0; i >= 0; i++)
            {
                if (Entities[i].IsUsed() != true)
                {
                    break;
                }
                else
                {
                    Entities[i].Draw(gameTime, spriteBatch);
                }
            }
            player.Draw(gameTime, spriteBatch);
        }
    }
}
