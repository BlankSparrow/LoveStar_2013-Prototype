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

    class Level : Microsoft.Xna.Framework.DrawableGameComponent
    {
        ContentManager content;
        private Game game;

        private int levelNumber;
        private int previousLevelNumber;
        private Texture2D LevelBackground;
        private Vector2 LevelSize;
        private bool reloadLevel;

        public int Day1;
        public int Day2;
        public int Day3;

        public Vector2 getLevelSize()
        {
            return this.LevelSize;
        }

        public void changeLevelTo(int levelNumber)
        {
            this.levelNumber = levelNumber;
            this.reloadLevel = true;
        }

        public int getLevelNumber()
        {
            return this.levelNumber;
        }

        public bool shouldPlayerShift()
        {
            return this.shiftCharacter;
        }
        public Vector2 getPlayerStartPosition()
        {
            this.shiftCharacter = false;
            return this.SetStartPosition;
        }

        private bool exitLeft;
        private bool exitRight;
        private Vector2 SetStartPosition;
        private bool shiftCharacter;



        public Level(Game game, GraphicsDeviceManager graphics)
            : base(game)
        {
            this.game = game;
        }

        public void LoadContent(IServiceProvider serviceProvider, ContentManager content)
        {
            this.content = content;
            this.levelNumber = 1;
            this.reloadLevel = true;
            this.LevelBackground = content.Load<Texture2D>("GameMode/Level/Level_1");
        }

        public Level Update(GameTime gameTime, Tools.KeyPress keyPress, Level Level)
        {
            if (reloadLevel == true)
            {
                this.LevelBackground = content.Load<Texture2D>("GameMode/Level/Level_" + levelNumber);
                setPlayerStartPosition(Level);
                shiftCharacter = true;
                reloadLevel = false;
            }
            Level = LevelRules(Level);
            previousLevelNumber = levelNumber;
            return Level;
        }

        public void setPlayerStartPosition(Level Level)
        {
            switch (Level.getLevelNumber())
            {
                case 1:
                    if (previousLevelNumber == 2) { this.SetStartPosition = new Vector2(400, 410); }
                    else if (previousLevelNumber == 3) { this.SetStartPosition = new Vector2(10, 410); }
                    else { this.SetStartPosition = new Vector2(400, 410); }
                    break;
                case 2:
                    if (previousLevelNumber == 1) { this.SetStartPosition = new Vector2(600, 410); }
                    break;
                case 3:
                    if (previousLevelNumber == 1) { this.SetStartPosition = new Vector2(1990, 410); }
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
            }
        }

        private Level LevelRules(Level Level)
        {
            this.LevelSize = new Vector2(this.LevelBackground.Width, this.LevelBackground.Height);
            switch (Level.getLevelNumber())
            {
                case 1:
                    this.exitLeft = true;
                    this.exitRight = false;
                    break;
                case 2:
                    this.exitLeft = true;
                    this.exitRight = true;
                    break;
                case 3:
                    this.exitLeft = true;
                    this.exitRight = true;
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
            }
            return Level;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(LevelBackground, Vector2.Zero, Color.White);
        }
    }
}
