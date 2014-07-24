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
    public partial class Elements
    {
        //---------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------Load Content-------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        protected void LoadElement(int element_reference)
        {
            Load_TextBox();
            Load_Interaction_Symbol();

            
            // ---------------------------------------------------------99-------------------------------------------------------
            if (element_reference == 99)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.05f, true);
                //secondary_Texture = content.Load<Texture2D>("Sprites/Element/99/Block");
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 1;

                boundingBox_Outer = new Rectangle(XPos - Tiles.Width, YPos - Tiles.Height * 15, Tiles.Width * 3, Tiles.Height * 30);
                //secondary_Rectangle = new Rectangle(XPos+ 25, YPos +25, secondary_Texture.Width, secondary_Texture.Height);
                //secondary_Opacity = 1;
            }
            // ---------------------------------------------------------100-------------------------------------------------------
            if (element_reference == 100)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.1f, true);
                animation_1 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_1"), 0.05f, false);
                animation_2 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_2"), 0.1f, true);
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 0;

                boundingBox_Inner = new Rectangle(XPos - animation_0.FrameWidth / 2 + 200, YPos - animation_0.FrameHeight + 350, Tiles.Width, Tiles.Height * 2);
            }

            // ---------------------------------------------------------101-------------------------------------------------------
            if (element_reference == 101)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.1f, true);
                animation_1 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_1"), 0.05f, false);
                animation_2 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_2"), 0.05f, false);
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 0;

                boundingBox_Inner = new Rectangle(XPos - animation_0.FrameWidth / 2 + 200, YPos - animation_0.FrameHeight + 350, Tiles.Width, Tiles.Height * 2);
            }

            // ---------------------------------------------------------102-------------------------------------------------------
            if (element_reference == 102)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.1f, false);
                animation_1 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_1"), 0.1f, false);
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 0;

                boundingBox_Inner = new Rectangle(XPos, YPos, animation_0.FrameWidth, animation_0.FrameHeight);
            }

            // ---------------------------------------------------------103-------------------------------------------------------
            if (element_reference == 103)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.1f, false);
                animation_1 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_1"), 0.1f, false);
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 0;

                boundingBox_Inner = new Rectangle(XPos, YPos, animation_0.FrameWidth, animation_0.FrameHeight);
            }

            // ---------------------------------------------------------104-------------------------------------------------------
            if (element_reference == 104)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.1f, false);
                animation_1 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_1"), 0.1f, false);
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 0;

                boundingBox_Inner = new Rectangle(XPos, YPos, animation_0.FrameWidth, animation_0.FrameHeight);
            }
            
            // ---------------------------------------------------------105-------------------------------------------------------
            if (element_reference == 105)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.1f, false);
                animation_1 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_1"), 0.1f, false);
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 0;

                boundingBox_Inner = new Rectangle(XPos, YPos, animation_0.FrameWidth, animation_0.FrameHeight);
            }

            // ---------------------------------------------------------106-------------------------------------------------------
            if (element_reference == 106)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.1f, false);
                animation_1 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_1"), 0.1f, false);
                animation_2 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_2"), 0.1f, false);
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 0;

                boundingBox_Inner = new Rectangle(XPos, YPos, animation_0.FrameWidth, animation_0.FrameHeight);
            }

            // ---------------------------------------------------------107-------------------------------------------------------
            if (element_reference == 107)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.1f, true);
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 0;

                boundingBox_Inner = new Rectangle(XPos, YPos, animation_0.FrameWidth, animation_0.FrameHeight);
            }

            // ---------------------------------------------------------108-------------------------------------------------------
            if (element_reference == 108)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.1f, true);
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 0;
                boundingBox_Inner = new Rectangle(XPos, YPos, Tiles.Width, Tiles.Height);
            }

            // ---------------------------------------------------------109-------------------------------------------------------
            if (element_reference == 109)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.1f, true);
                animation_1 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_1"), 0.1f, false);
                animation_2 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_2"), 0.1f, false);
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 0;
                boundingBox_Inner = new Rectangle(XPos - 100, YPos - 200, Tiles.Width + 200, Tiles.Height + 200);
                boundingBox_Outer = new Rectangle(XPos - 300, YPos - 250, animation_0.FrameWidth + 350, animation_0.FrameHeight);
            }

            // ---------------------------------------------------------110-------------------------------------------------------
            if (element_reference == 110)
            {
                animation_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/" + element_reference + "/" + element_reference + "_0"), 0.1f, true);
                sprite.PlayAnimation(animation_0);
                sprite_Opacity = 0;
                boundingBox_Inner = new Rectangle(XPos, YPos, Tiles.Width, Tiles.Height);
            }
        }


        //---------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------Update-------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void Update(Game_Mode game_Mode, GameTime gameTime, KeyPress keyPress, Rectangle player_BoundingBox, Vector2 player_Position)
        {

            Update_Interaction_Symbol();
            this.player_Position = player_Position;

            // ---------------------------------------------------------99-------------------------------------------------------
            if (element_reference == 99)
            {
                interact_display = false;
                if (boundingBox_Outer.Intersects(player_BoundingBox) == true)
                {
                    interact_display = false;
                    Vector2 Depth = Base_Components.RectangleExtensions.GetIntersectionDepth(boundingBox_Outer, player_BoundingBox);

                    if (Depth.X >= 0)
                    {
                        sprite_Opacity = 1 - (Depth.X / 100);
                        if (Depth.X >= 75)
                        {
                            secondary_Opacity = 1 - ((Depth.X - 75) / 25);
                        }
                        else
                        {
                            secondary_Opacity = 1;
                        }
                    }
                    else if (Depth.X < 0)
                    {
                        sprite_Opacity = 1 + (Depth.X / 100);
                        if (Depth.X < -75)
                        {
                            secondary_Opacity = 1 + ((Depth.X + 75) / 25);
                        }
                        else
                        {
                            secondary_Opacity = 1;
                        }
                    }

                }
                else
                {
                    sprite_Opacity = 1;
                    secondary_Opacity = 1;
                }
            }
            // ---------------------------------------------------------100-------------------------------------------------------
            if (element_reference == 100)
            {
                if (boundingBox_Inner.Intersects(player_BoundingBox) == true)
                {
                    if (interact_Stage == 0)
                    {
                        interact_display = true;
                        if (keyPress.key_X == 1)
                        {
                            sprite.PlayAnimation(animation_1);
                            interact_Stage += 1;
                        }
                    }
                    else if (interact_Stage == 1)
                    {
                        interact_display = false;
                        if (sprite.FrameIndex == 7)
                        {
                            sprite.PlayAnimation(animation_2);
                            interact_Stage += 1;
                        }
                    }
                    else if (interact_Stage == 2)
                    {
                        interact_display = true;
                        if (keyPress.key_X == 1)
                        {
                            interact_Stage += 1;
                        }
                    }
                    // Change Level
                    else if (interact_Stage == 3)
                    {
                        interact_display = false;
                        game_Mode.next_Level = 17;
                        game_Mode.change_level_bool = true;
                    }
                }
                else
                {
                    interact_display = false;
                }
            }

            // ---------------------------------------------------------101-------------------------------------------------------
            if (element_reference == 101)
            {
                if (boundingBox_Inner.Intersects(player_BoundingBox) == true)
                {
                    if (interact_Stage == 0)
                    {
                        interact_display = true;
                        if (keyPress.key_X == 1)
                        {
                            sprite.PlayAnimation(animation_1);
                            interact_Stage += 1;
                        }
                    }
                    else if (interact_Stage == 1)
                    {
                        interact_display = false;
                        if (sprite.FrameIndex == 7)
                        {
                            interact_Stage += 1;
                        }
                    }
                    else if (interact_Stage == 2)
                    {
                        interact_display = true;
                        if (keyPress.key_X == 1)
                        {
                            sprite.PlayAnimation(animation_2);
                            interact_Stage += 1;
                        }
                    }
                    else if (interact_Stage == 3)
                    {
                        interact_display = false;
                        if (sprite.FrameIndex == 7)
                        {
                            interact_Stage += 1;
                        }
                    }
                    else if (interact_Stage == 4)
                    {
                        interact_display = true;
                        if (keyPress.key_X == 1)
                        {
                            interact_Stage += 1;
                        }
                    }
                        // Change Level
                    else if (interact_Stage == 5)
                    {
                        interact_display = false;
                        game_Mode.next_Level = 15;
                        game_Mode.change_level_bool = true;
                    }
                }
                else
                {
                    interact_display = false;
                }
            }
            // ---------------------------------------------------------102-------------------------------------------------------
            if (element_reference == 102)
            {
                if (boundingBox_Inner.Intersects(player_BoundingBox) == true)
                {
                    if (interact_Stage == 0)
                    {
                        interact_display = true;
                        if (keyPress.key_X == 1)
                        {

                            sprite.PlayAnimation(animation_1);
                            game_Mode.Change_Outfit("a10");
                            interact_Stage += 1;
                        }
                    }
                    else if (interact_Stage == 1)
                    {
                        interact_display = false;
                    }
                }
                else
                {
                    interact_display = false;
                }
            }

            // ---------------------------------------------------------103-------------------------------------------------------
            if (element_reference == 103)
            {
                if (boundingBox_Inner.Intersects(player_BoundingBox) == true)
                {
                    if (interact_Stage == 0)
                    {
                        interact_display = true;
                        if (keyPress.key_X == 1)
                        {
                            sprite.PlayAnimation(animation_1);
                            if (game_Mode.Outfit_Check() == "a10")
                            {
                                game_Mode.Change_Outfit("a11");
                            }
                            else
                            {
                                game_Mode.Change_Outfit("a01");
                            }
                            interact_Stage += 1;
                        }
                    }
                    else if (interact_Stage == 1)
                    {
                        interact_display = false;
                    }
                }
                else
                {
                    interact_display = false;
                }
            }

            // ---------------------------------------------------------104-------------------------------------------------------
            if (element_reference == 104)
            {
                if (boundingBox_Inner.Intersects(player_BoundingBox) == true)
                {
                    if (interact_Stage == 0)
                    {
                        interact_display = true;
                        if (keyPress.key_X == 1)
                        {
                            sprite.PlayAnimation(animation_1);
                            interact_Stage += 1;
                        }
                    }
                    else if (interact_Stage == 1)
                    {
                        interact_display = false;
                        if (sprite.FrameIndex == 8)
                        {
                            interact_Stage += 1;
                        }
                    }
                    else if (interact_Stage == 2)
                    {
                        interact_display = true;
                        if (keyPress.key_X == 1)
                        {
                            interact_Stage += 1;
                        }
                    }
                    // Change Level
                    else if (interact_Stage == 3)
                    {
                        interact_display = false;
                        game_Mode.next_Level = 11;
                        game_Mode.change_level_bool = true;
                    }
                }
                else
                {
                    interact_display = false;
                }
            }

            // ---------------------------------------------------------105-------------------------------------------------------
            if (element_reference == 105)
            {
                if (boundingBox_Inner.Intersects(player_BoundingBox) == true)
                {
                    if (interact_Stage == 0)
                    {
                        interact_display = true;
                        if (keyPress.key_X == 1)
                        {
                            sprite.PlayAnimation(animation_1);
                            if (game_Mode.Outfit_Check() == "a01")
                            {
                                game_Mode.Change_Outfit("g01");
                            }
                            else if (game_Mode.Outfit_Check() == "a10")
                            {
                                game_Mode.Change_Outfit("g10");
                            }
                            else if (game_Mode.Outfit_Check() == "a11")
                            {
                                game_Mode.Change_Outfit("g11");
                            }
                            else
                            {
                                game_Mode.Change_Outfit("g00");
                            }
                            interact_Stage += 1;
                        }
                    }
                    else if (interact_Stage == 1)
                    {
                        interact_display = false;
                    }
                }
                else
                {
                    interact_display = false;
                }
            }

            // ---------------------------------------------------------106-------------------------------------------------------
            if (element_reference == 106)
            {
                //Self_Animating
                if (sprite.Animation == animation_0 && sprite.FrameIndex == 5)
                {
                    sprite.PlayAnimation(animation_1);
                }
                if (sprite.Animation == animation_1 && sprite.FrameIndex == 5)
                {
                    sprite.PlayAnimation(animation_2);
                }
                if (sprite.Animation == animation_2 && sprite.FrameIndex == 5)
                {
                    sprite.PlayAnimation(animation_0);
                }
            }

            // ---------------------------------------------------------107-------------------------------------------------------
            if (element_reference == 107)
            {
                //Self_Animating
                on_top_animation = true;
            }

            // ---------------------------------------------------------108-------------------------------------------------------
            if (element_reference == 108)
            {
            }

            // ---------------------------------------------------------109-------------------------------------------------------
            if (element_reference == 109)
            {
                if (interact_Stage == 0 && boundingBox_Outer.Intersects(player_BoundingBox) == true)
                {
                    sprite.PlayAnimation(animation_1);
                }
                if (interact_Stage == 0 && boundingBox_Inner.Intersects(player_BoundingBox) == true)
                {
                    interact_Stage = 1;
                }
                if (interact_Stage == 1 && sprite.Animation == animation_1 && sprite.FrameIndex == 4)
                {
                    game_Mode.halt_Movement(true);
                    sprite.PlayAnimation(animation_2);
                    interact_Stage = 3;
                }
                if (interact_Stage == 3)
                {
                    game_Mode.halt_Movement(true);
                    if (game_Mode.Outfit_Check() == "g00" || game_Mode.Outfit_Check() == "g01" ||
                        game_Mode.Outfit_Check() == "g10" || game_Mode.Outfit_Check() == "g11")
                    {
                        write_text("You have a gun");
                        if (keyPress.key_X == 1)
                        {
                            close_text(game_Mode);
                            interact_Stage = 6;
                            game_Mode.halt_Movement(false);
                        }
                    }
                    else
                    {
                        write_text("You have no gun");
                        game_Mode.halt_Movement(true);

                        if (keyPress.key_X == 1)
                        {
                            close_text(game_Mode);
                            interact_Stage = 5;
                            game_Mode.halt_Movement(false);
                        }
                    }

                    

                }
                if (interact_Stage == 5)
                {
                    game_Mode.next_Level = 22;
                    game_Mode.change_level_bool = true;

                }

            }

            // ---------------------------------------------------------110-------------------------------------------------------
            if (element_reference == 110)
            {
                //Self_Animating
            }
        }








        //---------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------General Code-------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void Load_TextBox()
        {
            text_sprite = content.Load<SpriteFont>("Fonts/Text_Box_Font");
            // Load Random Text_Box
            int num_1 = RandomNumber(0, 2);
            int num_2 = RandomNumber(0, 10);

            text_Box = content.Load<Texture2D>("Sprites/Element/TextBox/TB_" + num_1 + "" + num_2);
        }

        private void Load_Interaction_Symbol()
        {
            //Load Interaction Symbol
            interact_0 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/Interact/00"), 0.05f, false);
            interact_1 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/Interact/01"), 0.05f, false);
            interact_2 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/Interact/02"), 0.05f, true);
            interact_3 = new Base_Components.Animation(content.Load<Texture2D>("Sprites/Element/Interact/03"), 0.05f, false);

            interact_Ani.PlayAnimation(interact_0);
            interact_Dia_Ani.PlayAnimation(interact_2);
        }

        private void Update_Interaction_Symbol()
        {
            if (interact_display == true)
            {
                if (interact_Ani.Animation == interact_0 || interact_Ani.Animation == interact_3)
                {
                    interact_Ani.PlayAnimation(interact_1);
                }

                else if (interact_Ani.Animation == interact_1)
                {
                    if (interact_Ani.FrameIndex == 6)
                    {
                        interact_Ani.PlayAnimation(interact_2);
                    }
                }
            }
            else if (interact_display == false)
            {
                if (interact_Ani.Animation != interact_0)
                {
                    interact_Ani.PlayAnimation(interact_3);
                    if (interact_Ani.FrameIndex == 3)
                    {
                        interact_Ani.PlayAnimation(interact_0);
                    }
                }
            }
        }

        private void write_text(string text_0)
        {
            disp_Text = true;
            this.text_0 = text_0;

            text_0_pos = new Vector2(0, (text_Box.Height / 2) - (text_sprite.MeasureString(text_0).Y / 2) + 13);
        }
        private void write_text(string text_0, string text_1)
        {
            disp_Text = true;
            this.text_0 = text_0;
            this.text_1 = text_1;

            text_0_pos = new Vector2(0, (text_Box.Height / 2) - (text_sprite.MeasureString(text_0).Y) + 10);
            text_1_pos = new Vector2(0, (text_Box.Height / 2) - (text_sprite.MeasureString(text_1).Y) + 45);
        }
        private void close_text(Game_Mode game_Mode)
        {
            disp_Text = false;
            game_Mode.halt_Movement(false);
        }
    }
}
