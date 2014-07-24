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
        // Structure
        struct GameObjectsStore
        {
            public List<Tiles> tileStore;
            public Tiles[,] tileMap;
            public int[,] tileMapOrigin;
            public Player player;
            public Texture2D[] backgrounds;
            public List<Elements> elementStore;

            public GameObjectsStore(List<Tiles> tileStore, Tiles[,] tileMap, int[,] tileMapOrigin, Player player, Texture2D[] backgrounds, List<Elements> elementStore)
            {
                this.tileStore = tileStore;
                this.tileMap = tileMap;
                this.tileMapOrigin = tileMapOrigin;
                this.player = player;
                this.backgrounds = backgrounds;
                this.elementStore = elementStore;
            }
        }

        struct Alter_Player
        {
            public string outfit_Set;

            public Alter_Player(string outfit_Set)
            {
                this.outfit_Set = outfit_Set;
            }
        }

        // Varibles

        const string start_Outfit = "a00";
        const int start_Level = 1;
        private int current_Level;
        public int next_Level;
        public bool change_level_bool = false;

        private int level_exit_0;
        private int level_exit_1;
        private int level_exit_2;
        private int level_exit_3;
        private int level_exit_4;
        private int level_exit_5;
        private int level_exit_6;
        private int level_exit_7;
        private int level_exit_8;
        private int level_exit_9;

        private Vector2 start;
        private static readonly Point InvalidPosition = new Point(-1, -1);

        // Assets
        private int total_Backgrounds;
        private int total_Foregrounds;

        private Texture2D Background_03;
        private Texture2D Background_02;
        private Texture2D Background_01;
        private Texture2D Background_00;
        // Where the tiles sit
        private Texture2D Foreground_00;
        private Texture2D Foreground_01;
        private Texture2D Foreground_02;



        

        private void Building_Restart()
        {
            alter_Player.outfit_Set = start_Outfit;
            current_Level = start_Level;
            define_varibles(current_Level);
            Load_Backgrounds(total_Backgrounds, total_Foregrounds);
            Load_Tiles();
            Base_Components.Camera.offset = player.Position - game_Window_Size / 2;
        }
        
        private void Refresh()
        {
            current_Level = next_Level;
            player_flip = player.flip;
            define_varibles(current_Level);
            Load_Backgrounds(total_Backgrounds, total_Foregrounds);
            Load_Tiles();
            next_Level = 0;
            player.flip = player_flip;
            Base_Components.Camera.offset = player.Position - game_Window_Size/2;
        }

        private void Load_Tiles()
        {
            gameObjectsStore.elementStore = new List<Elements>();
            gameObjectsStore.tileMap = new Tiles[Map_Width, Map_Height];

            for (int y = 0; y < Map_Height; ++y)
            {
                for (int x = 0; x < Map_Width; ++x)
                {
                    int tileNumber = gameObjectsStore.tileMapOrigin[y, x];
                    gameObjectsStore.tileMap[x, y] = LoadTile(tileNumber, x, y);
                }
            }
        }

        private Tiles LoadTile(int tileType, int x, int y)
        {
            if (tileType > 99)
            {
                return LoadElement(x, y, tileType);
            }
            else
            {
                switch (tileType)
                {
                    // Blank Passible Tile
                    case 0:
                        return LoadTile("null", TileType.Passable, TileAction.None);

                    // Solid Impassible Tile
                    case 1:
                        return LoadTile("Solid", TileType.Impassable, TileAction.None);

                    // Blank Impassible Tile
                    case 2:
                        return LoadTile("null", TileType.Impassable, TileAction.None);

                    // Blank Platform Tile
                    case 3:
                        return LoadTile("null", TileType.Platform, TileAction.None);

                    // Start Tile
                    case 4:
                        return LoadStartTile(x, y);

                    // Crawl Tile
                    case 5:
                        return LoadTile("null", TileType.Passable, TileAction.Crawl);

                    case 99:
                        return LoadBlock(x, y, tileType);

                    // Unknown tile type character
                    default:
                        throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", tileType, x, y));
                }
            }
        }

        private Tiles LoadTile(string name, TileType tileType, TileAction tileAction)
        {
            return new Tiles(Content.Load<Texture2D>("Tiles/" + name), tileType, tileAction);
        }

        private Tiles LoadStartTile(int x, int y)
        {
            start = Base_Components.RectangleExtensions.GetBottomCenter(GetBounds(x, y));
            player = new Player(this, start, alter_Player.outfit_Set);
            gameObjectsStore.player = player;

            return new Tiles(Content.Load<Texture2D>("Tiles/null"), TileType.Passable, TileAction.Entrance);
        }

        private Tiles LoadBlock(int x, int y, int element_reference)
        {
            gameObjectsStore.elementStore.Add(new Elements(this, content, x, y, element_reference));
            return new Tiles(Content.Load<Texture2D>("Tiles/null"), TileType.Impassable, TileAction.None);
        }

        private Tiles LoadElement(int x, int y, int element_reference)
        {
            gameObjectsStore.elementStore.Add(new Elements(this,content, x, y, element_reference));
            return new Tiles(Content.Load<Texture2D>("Tiles/null"), TileType.Passable, TileAction.None);
        }

        private void Load_Backgrounds(int backgrounds, int foregrounds)
        {
            if (backgrounds >= 1)
            {
                Background_00 = content.Load<Texture2D>("Levels/" + current_Level + "/Backgrounds/" + current_Level + "_b00");
                if (backgrounds >= 2)
                {
                    Background_01 = content.Load<Texture2D>("Levels/" + current_Level + "/Backgrounds/" + current_Level + "_b01");
                    if (backgrounds >= 3)
                    {
                        Background_02 = content.Load<Texture2D>("Levels/" + current_Level + "/Backgrounds/" + current_Level + "_b02");
                        if (backgrounds >= 4)
                        {
                            Background_03 = content.Load<Texture2D>("Levels/" + current_Level + "/Backgrounds/" + current_Level + "_b03");
                        }
                    }
                }
            }
            if (foregrounds >= 1)
            {
                Foreground_00 = content.Load<Texture2D>("Levels/" + current_Level + "/Foregrounds/" + current_Level + "_f00");
                if (foregrounds >= 2)
                {
                    Foreground_01 = content.Load<Texture2D>("Levels/" + current_Level + "/Foregrounds/" + current_Level + "_f01");
                    if (foregrounds >= 3)
                    {
                        Foreground_02 = content.Load<Texture2D>("Levels/" + current_Level + "/Foregrounds/" + current_Level + "_f02");
                    }
                }
            }

            gameObjectsStore.backgrounds = new Texture2D[7] { Background_00, Background_01, Background_02, Background_03, Foreground_00, Foreground_01, Foreground_02 };
        }

        private void Draw_Game(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawBackgrounds(gameTime, spriteBatch);
            DrawTiles(spriteBatch);
            Draw_Elements(gameTime, spriteBatch);
            player.Draw(gameTime, spriteBatch);
            Draw_Elements_Front(gameTime, spriteBatch);
        }

        private void Draw_Elements(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < gameObjectsStore.elementStore.Count(); i++)
            {
                gameObjectsStore.elementStore[i].Draw(gameTime, spriteBatch);
            }
        }

        private void Draw_Elements_Front(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < gameObjectsStore.elementStore.Count(); i++)
            {
                gameObjectsStore.elementStore[i].Draw_2(gameTime, spriteBatch, game_Window_Size);
            }
        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {            // For each tile position
            for (int y = 0; y < Map_Height; ++y)
            {
                for (int x = 0; x < Map_Width; ++x)
                {
                    // If there is a visible tile in that position
                    Texture2D texture = gameObjectsStore.tileMap[x, y].texture;
                    if (texture != null)
                    {
                        // Draw it in screen space.
                        Vector2 position = new Vector2(x, y) * Tiles.Size;
                        spriteBatch.Draw(texture, position, Color.White);
                    }
                }
            }
        }

        // need to be rewritten
        private void DrawBackgrounds(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < total_Backgrounds; ++x)
            {
                spriteBatch.Draw(gameObjectsStore.backgrounds[x], new Vector2(2,4), Color.White);
            }
            for (int y = 0; y < total_Foregrounds; ++y)
            {
                spriteBatch.Draw(gameObjectsStore.backgrounds[y + 4], Vector2.Zero, Color.White);
            }
        }
    }
}
