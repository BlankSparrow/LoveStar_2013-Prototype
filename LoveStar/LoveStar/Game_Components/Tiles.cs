using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoveStar.Game_Components
{
    public enum TileType
    {
        Passable = 0,
        Impassable = 1,
        Platform = 2,
    }

    public enum TileAction
    {
        Entrance = 0,
        Npc = 1,
        None = 2,
        Crawl = 3,
    }

    public class Tiles
    {
        // Constants
        public static int Width = 100;
        public static int Height = 100;

        public static readonly Vector2 Size = new Vector2(Width, Height);

        // Varibles
        public Texture2D texture;
        public TileType tileType;
        public TileAction tileAction;


        public Tiles(Texture2D texture, TileType tileType, TileAction tileAction)
        {
            this.texture = texture;
            this.tileType = tileType;
            this.tileAction = tileAction;
            this.getWidth();
            this.getHeight();
        }

        // Get Methods
        public int getWidth()
        {
            return Width;
        }

        public int getHeight()
        {
            return Height;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public TileType getTileType()
        {
            return tileType;
        }

        public TileAction getTileAction()
        {
            return tileAction;
        }

    }
}
