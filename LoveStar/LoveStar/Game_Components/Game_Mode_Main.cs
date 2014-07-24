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


namespace LoveStar.Game_Components
{
    public partial class Game_Mode
    {
        enum Playing_State
        {
            fade_in_state,
            fade_out_state,
            loading_state,
            game_state,
            pause_state,
        }

        ContentManager content;
        GameObjectsStore gameObjectsStore;
        Alter_Player alter_Player;
        
        
        // Components
        Player player;
        Global_Variables global_Varibles = new Global_Variables();

        // Assets
        Texture2D screen_Background;
        Texture2D dev_Base;
        Texture2D screen_Fade;
        SpriteFont Dev_Font;
        SpriteFont loading_Font;

        // Variables
        private Playing_State playing_State = Playing_State.loading_state;
        private bool next_Window_State_Bool;

        const double fade_Delay = .013;
        const float fadeIncrement = 0.01f;
        private float fade_AlphaValue;
        private double FadeDelay;
        private SpriteEffects player_flip;

        private bool reload = true;
        private bool is_paused = false;
        private bool dev_display = false;
        private bool halt_movement = false;

        private Vector2 game_Window_Size;

        public ContentManager Content
        {
            get { return content; }
        }

        public Game_Mode(GraphicsDeviceManager graphics)
        {
            game_Window_Size = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public void LoadContent(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");
        }

        public void Change_Outfit(string outfit)
        {
            alter_Player.outfit_Set = outfit;
            player.Change_Outfit(alter_Player.outfit_Set);
        }

        public string Outfit_Check()
        {
            return alter_Player.outfit_Set;
        }

        public void halt_Movement(bool movement_Stopped)
        {
            halt_movement = movement_Stopped;
            if (movement_Stopped == true)
            {
                player.animation_To_Idle();
            }
        }

        public void Reload()
        {
            Pause_Reload();
            screen_Background = content.Load<Texture2D>("Fades/White");
            dev_Base = content.Load<Texture2D>("Dev/Dev_Base");
            screen_Fade = content.Load<Texture2D>("Fades/Black");
            Dev_Font = content.Load<SpriteFont>("Fonts/Dev_Font");
            loading_Font = content.Load<SpriteFont>("Fonts/Loading_Font");

            fade_AlphaValue = 0f;
            FadeDelay = 0;
            
            Building_Restart();
            playing_State = Playing_State.fade_in_state; 
            is_paused = false;

            next_Window_State_Bool = false;
            reload = false;
        }

        public void Dismiss()
        {
            reload = true;
            Content.Unload();
        }

        public Window_Return_Info Update(GameTime gameTime, KeyPress keyPress)
        {
            Window_Return_Info window_Return_Info;
            window_Return_Info.windowTransition = false;
            window_Return_Info.newState = Game_Window_State.Game_Mode_State;
            
            if (reload == true)
            {
                Reload();
            }

            if (change_level_bool == true)
            {
                playing_State = Playing_State.fade_out_state;
                change_level_bool = false;
            }

            // Change Game_States between Game and Pause States
            if (keyPress.key_Esc == 1)
            {
                if (is_paused == true && playing_State == Playing_State.pause_state) { playing_State = Playing_State.game_state; is_paused = false; }
                else if (is_paused == false && playing_State == Playing_State.game_state) { playing_State = Playing_State.pause_state; is_paused = true; }
            }

            // Display Dev_Info
            if (keyPress.key_T == 1)
            {
                if (dev_display == true) { dev_display = false; }
                else if (dev_display == false) { dev_display = true; }
            }

            CameraStroll();

            switch (playing_State)
            {
                case Playing_State.fade_in_state:
                    
                    FadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                    if (FadeDelay <= 0)
                    {
                        FadeDelay = fade_Delay;
                        fade_AlphaValue += fadeIncrement;

                        if (fade_AlphaValue >= 1)
                        {
                            fade_AlphaValue = MathHelper.Clamp(fade_AlphaValue, 0, 1);
                            playing_State = Playing_State.game_state;
                        }
                    }
                    break;

                case Playing_State.game_state:
                    player.Update(gameTime, keyPress, halt_movement);
                    
                    for (int i = 0; i < gameObjectsStore.elementStore.Count(); i++)
                    {
                        gameObjectsStore.elementStore[i].Update( this, gameTime, keyPress, player.BoundingRectangle, player.Position);
                    }

                    Exits_Map_Based();
                    break;

                case Playing_State.fade_out_state:

                    player.animation_To_Idle();
                    FadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                    if (FadeDelay <= 0)
                    {
                        FadeDelay = fade_Delay;
                        fade_AlphaValue -= fadeIncrement;

                        if (fade_AlphaValue <= 0)
                        {
                            if (next_Window_State_Bool == true)
                            {
                                window_Return_Info.windowTransition = true;
                                window_Return_Info.newState = Game_Window_State.Splash_Screen_State;
                                Dismiss();
                                fade_AlphaValue = 0;
                            }
                            else
                            {
                                fade_AlphaValue = MathHelper.Clamp(fade_AlphaValue, 0, 1);
                                playing_State = Playing_State.loading_state;
                            }
                        }
                    }

                    break;
                case Playing_State.loading_state:
                    Refresh();
                    playing_State = Playing_State.fade_in_state;
                    break;

                case Playing_State.pause_state:
                    Pause_Update(keyPress);
                    break;
            }

            return window_Return_Info;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (reload == true)
            {
                Reload();
            }

            spriteBatch.Draw(screen_Background, new Rectangle((int)Base_Components.Camera.offset.X -10, (int)Base_Components.Camera.offset.Y - 10, (int)game_Window_Size.X + 20, (int)game_Window_Size.Y + 20), Color.White);

            Draw_Game(gameTime, spriteBatch);

            switch (playing_State)
            {
                case Playing_State.game_state:
                    break;

                case Playing_State.loading_state:
                    Loading(spriteBatch);
                    break;

                case Playing_State.pause_state:
                    Pause_Draw(spriteBatch);
                    break;
            }

            if (dev_display == true)
            {
                Dev_Draw(spriteBatch);
            }
            if (is_paused == true)
            {
                Pause_Draw(spriteBatch);
            }

            spriteBatch.Draw(screen_Fade, new Rectangle((int)Base_Components.Camera.offset.X - 10, (int)Base_Components.Camera.offset.Y -10,
                (int)game_Window_Size.X + 20, (int)game_Window_Size.Y + 20), Color.Lerp(Color.White, Color.Transparent, fade_AlphaValue));
        }

        private void Loading(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(loading_Font, "loading", new Vector2(
                (loading_Font.MeasureString("loading").X / 2) + (game_Window_Size.X / 2) + Base_Components.Camera.offset.X,
                (loading_Font.MeasureString("loading").Y / 2) + (game_Window_Size.Y / 2) + Base_Components.Camera.offset.Y),
                Color.White);
        }

        private void Dev_Draw(SpriteBatch spriteBatch)
        {
            Vector2 Dev_Position = Base_Components.Camera.offset + game_Window_Size - new Vector2(dev_Base.Width, dev_Base.Height);

            spriteBatch.Draw(dev_Base, new Rectangle(
                (int)Dev_Position.X - 20, (int)Dev_Position.Y - 20,
                dev_Base.Width, dev_Base.Height), Color.White);

            //spriteBatch.DrawString(Dev_Font, "Lvl " + current_Level + "", Dev_Position, Color.White);
            spriteBatch.DrawString(Dev_Font, "MapX - " + (Map_Width * Tiles.Width) + "", Dev_Position + new Vector2( 0, 0), Color.White);
            spriteBatch.DrawString(Dev_Font, "MapY - " + (Map_Height * Tiles.Height) + "", Dev_Position + new Vector2(125, 0), Color.White);
            spriteBatch.DrawString(Dev_Font, "Clvl - " + current_Level + "", Dev_Position + new Vector2(0, 20), Color.White);
            spriteBatch.DrawString(Dev_Font, "Nlvl - " + next_Level + "", Dev_Position + new Vector2(125,20), Color.White);
            spriteBatch.DrawString(Dev_Font, "OFit - " + alter_Player.outfit_Set + "", Dev_Position + new Vector2(0, 40), Color.White);
            spriteBatch.DrawString(Dev_Font, " - ", Dev_Position + new Vector2(125, 40), Color.White);
        }

        public void CameraStroll()
        {
            // Camera Limits
            if (Base_Components.Camera.offset.X < Tiles.Width) { Base_Components.Camera.offset.X = Tiles.Width; }
            else if (Base_Components.Camera.offset.X > (Map_Width * Tiles.Width) - game_Window_Size.X - Tiles.Width)
            { Base_Components.Camera.offset.X = (Map_Width * Tiles.Width) - game_Window_Size.X - Tiles.Width; }

            if (Base_Components.Camera.offset.Y < Tiles.Height) { Base_Components.Camera.offset.Y = Tiles.Height; }
            else if (Base_Components.Camera.offset.Y > (Map_Height * Tiles.Height) - game_Window_Size.Y - Tiles.Height)
            { Base_Components.Camera.offset.Y = (Map_Height * Tiles.Height) - game_Window_Size.Y - Tiles.Height; }

            // X Movement
            if (player.Position.X <= ((game_Window_Size.X / 2) + Tiles.Width))
            {
                Base_Components.Camera.offset.X = (MathHelper.Lerp(Base_Components.Camera.offset.X, Tiles.Height, 0.1f));
            }
            else if (player.Position.X >= ((Map_Width * Tiles.Width) - (game_Window_Size.X / 2) - Tiles.Width))
            {
                Base_Components.Camera.offset.X = (MathHelper.Lerp(Base_Components.Camera.offset.X, ((Map_Width * Tiles.Width) - (game_Window_Size.X) - Tiles.Width), 0.06f));
            }
            else
            {
                Base_Components.Camera.offset.X = (MathHelper.Lerp(Base_Components.Camera.offset.X, player.Position.X - (game_Window_Size.X / 2), 0.06f));
            }

            // Y movement
            if (player.Position.Y <= ((game_Window_Size.Y / 7) * 4.3f) + Tiles.Height)
            {
                Base_Components.Camera.offset.Y = (MathHelper.Lerp(Base_Components.Camera.offset.Y, Tiles.Height, 0.07f));
            }
            else if (player.Position.Y >= (Map_Height * Tiles.Height) - ((game_Window_Size.Y / 7) * 2.7f) - Tiles.Height)
            {
                Base_Components.Camera.offset.Y = (MathHelper.Lerp(Base_Components.Camera.offset.Y, (Map_Height * Tiles.Height) - game_Window_Size.Y - Tiles.Height, 0.07f));
            }
            else
            {
                Base_Components.Camera.offset.Y = (MathHelper.Lerp(Base_Components.Camera.offset.Y, player.Position.Y - ((game_Window_Size.Y / 7) * 4.3f), 0.07f));
            }
        }
    }
}
