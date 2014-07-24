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
    struct Global_Variables
    {
        public int level_progress;

        public void level_refresh()
        {
            level_progress = 0;
        }
    }

    public partial class Elements
    {
        // Required Var
        ContentManager content;
        Global_Variables global_variables;

        private int XPos;
        private int YPos;
        private int element_reference;
        private Vector2 player_Position;

        // Interact Var
        private Base_Components.Animation interact_0;
        private Base_Components.Animation interact_1;
        private Base_Components.Animation interact_2;
        private Base_Components.Animation interact_3;
        private Base_Components.AnimationPlayer interact_Ani = new Base_Components.AnimationPlayer();
        private Base_Components.AnimationPlayer interact_Dia_Ani = new Base_Components.AnimationPlayer();

        private bool interact_display = false;

        // Text_Box
        Texture2D text_Box;
        private bool disp_Text;
        SpriteFont text_sprite;
        string text_0;
        string text_1;
        Vector2 text_0_pos;
        Vector2 text_1_pos;

        // Second Texture
        Texture2D secondary_Texture;
        private float secondary_Opacity;
        private Rectangle secondary_Rectangle;

        // Element Animation
        private Base_Components.Animation animation_0;
        private Base_Components.Animation animation_1;
        private Base_Components.Animation animation_2;
        private Base_Components.Animation animation_3;
        private Base_Components.Animation animation_4;
        private Base_Components.Animation animation_5;
        private Base_Components.AnimationPlayer sprite = new Base_Components.AnimationPlayer();

        private float sprite_Opacity;
        private bool on_top_animation;
        private Rectangle boundingBox_Inner;
        private Rectangle boundingBox_Outer;


        bool active = false;
        int interact_Stage = 0;

        public Elements(Game_Mode game_Mode, ContentManager content, int XPos, int YPos, int element_reference)
        {
            this.XPos = XPos * Tiles.Width;
            this.YPos = YPos * Tiles.Height;
            this.content = content;
            this.element_reference = element_reference;
            LoadElement(element_reference);
            global_variables.level_progress = 0;
        }

        // Update() is in Elements_Extended

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(interact_Texture, new Rectangle(player.Position.X + 50, YPos, texture.Width, texture.Height), Color.White);
            if (on_top_animation != true)
            {
                sprite.Draw(gameTime, spriteBatch, new Vector2(XPos + Tiles.Width / 2, YPos + Tiles.Height), SpriteEffects.None, sprite_Opacity);
            }
            if (secondary_Texture != null)
            {
                spriteBatch.Draw(secondary_Texture, secondary_Rectangle, Color.Lerp(Color.White, Color.Transparent, secondary_Opacity));
            }
        }

        public void Draw_2(GameTime gameTime, SpriteBatch spriteBatch, Vector2 game_Window_Size)
        {
            if (on_top_animation == true)
            {
                sprite.Draw(gameTime, spriteBatch, new Vector2(XPos + Tiles.Width / 2, YPos + Tiles.Height), SpriteEffects.None, sprite_Opacity);
            }

            if (disp_Text == true)
            {
                spriteBatch.Draw(text_Box, new Rectangle((int)Base_Components.Camera.offset.X + ((int)game_Window_Size.X / 2) - (text_Box.Width / 2),
                                                         (int)Base_Components.Camera.offset.Y + ((int)game_Window_Size.Y / 2) - (text_Box.Height) + 300,
                                                         text_Box.Width, text_Box.Height), Color.Lerp(Color.White, Color.Transparent, secondary_Opacity));

                Draw_Text(gameTime, spriteBatch, game_Window_Size);

                interact_Dia_Ani.Draw(gameTime, spriteBatch, new Vector2((int)Base_Components.Camera.offset.X + ((int)game_Window_Size.X / 2) + (text_Box.Width / 2 - 50),
                                                                     (int)Base_Components.Camera.offset.Y + ((int)game_Window_Size.Y / 2) + (text_Box.Height + 100)), 
                                                                     SpriteEffects.None, sprite_Opacity);
            }
            else
            {
                interact_Ani.Draw(gameTime, spriteBatch, new Vector2((int)player_Position.X + 30, (int)player_Position.Y - 190), SpriteEffects.None, sprite_Opacity);
            }
        }


        private void Draw_Text(GameTime gameTime, SpriteBatch spriteBatch, Vector2 game_Window_Size)
        {                                           
            if (text_0 != null)
            {
                spriteBatch.DrawString(text_sprite, text_0,
                    new Vector2((int)Base_Components.Camera.offset.X + ((int)game_Window_Size.X/2) - (text_sprite.MeasureString(text_0).X / 2),
                                (int)Base_Components.Camera.offset.Y + ((int)game_Window_Size.Y / 2) - (text_Box.Height) + 300 + text_0_pos.Y),    
                                Color.Black);
            }
            if (text_1 != null)
            {
                spriteBatch.DrawString(text_sprite, text_1,
                     new Vector2((int)Base_Components.Camera.offset.X + ((int)game_Window_Size.X / 2) - (text_sprite.MeasureString(text_1).X / 2),
                                 (int)Base_Components.Camera.offset.Y + ((int)game_Window_Size.Y / 2) - (text_Box.Height) + 300 + text_1_pos.Y),
                                 Color.Black);
            }
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
