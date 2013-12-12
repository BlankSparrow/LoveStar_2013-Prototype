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
    public class section
    {
        public Vector2 position;
        public float vx, vy, mass, angle;

        public section(Vector2 position, float angle, float mass)
        {
            this.position = position;
            this.angle = angle;
            this.vx = 0;
            this.vy = 0;
            this.mass = mass + 1;
        }
    }

    class Scarf : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private ContentManager content;
        private Game game;



        private Vector2 position;
        private Texture2D scarfTexture;
        private Texture2D scarfEnd;
        private Vector2 scarfSectionSize = new Vector2(10, 15);
        private int length = 1;
        private float gravity = 4;
        private float stiffness = 0.0f;
        private float dampening = 0.5f;

        List<section> scarf1 = new List<section>();
        List<section> scarf2 = new List<section>();

        public Scarf(Game game, Vector2 position)
            : base(game)
        {
            this.game = game;
            this.position = position;
            
            scarf1 = NumOfSections(30, 0.6f);
            scarf2 = NumOfSections(20, 0.2f);
        }

        public List<section> NumOfSections(int num, float mass)
        {
            List<section> sections = new List<section>();
            for (int i = 0; i < num; i++)
            {
                sections.Add(new section(position, (float)(Math.PI / 2), mass));
            }
            return sections;
        }

        public void LoadContent(IServiceProvider serviceProvider, ContentManager content)
        {
            this.content = content;
            scarfEnd = content.Load<Texture2D>("Scarf/scarfEnd");
            scarfTexture = CreateTexture();
        }

        public Texture2D CreateTexture()
        {
            Vector2 scarfSize = scarfSectionSize;
            Texture2D texture = new Texture2D(GraphicsDevice, (int)scarfSize.X, (int)scarfSize.Y);

            Color[] data = new Color[((int)scarfSize.X) * ((int)scarfSize.Y)];

            //Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
            {
                if (i % scarfSectionSize.X == 0 || i % scarfSectionSize.X == scarfSectionSize.X - 1 || i > (scarfSectionSize.X * scarfSectionSize.Y) - scarfSectionSize.X * 2)
                {
                    data[i] = Color.Black;
                }
                else
                {
                    data[i] = Color.White;
                }
            }

            texture.SetData(data);
            return texture;
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            this.position = position;

            updateScarf(gameTime, position, scarf1, 0.4f, 0.5f);
            updateScarf(gameTime, position, scarf2, 0.5f, 0.5f);
            base.Update(gameTime);
        }

        public void updateScarf(GameTime gameTime, Vector2 position, List<section> scarf, float lerpX, float lerpY)
        {
            scarf[0].angle = (float)Math.Atan2((double)(position.Y - scarf[0].position.Y), (double)(position.X - scarf[0].position.X));
            scarf[0].position = position;

            for (int i = 1; i < scarf.Count; i++)
            {
                float forceX = (scarf[i - 1].position.X - scarf[i - 1].position.X) * stiffness;
                float ax = forceX / scarf[i].mass;
                scarf[i].vx = dampening * (scarf[i].vx + ax);
                scarf[i].position.X += scarf[i].vx;

                float forceY = (scarf[i - 1].position.Y - scarf[i - 1].position.Y) * stiffness;
                forceY += gravity;
                float ay = forceY / scarf[i].mass;
                scarf[i].vy = dampening * (scarf[i].vy + ay);
                scarf[i].position.Y += scarf[i].vy;

                scarf[i].angle = (float)Math.Atan2(scarf[i - 1].position.Y - scarf[i].position.Y,
                    scarf[i - 1].position.X - scarf[i].position.X) + 1.5f;

                scarf[i].position.X = MathHelper.Lerp(scarf[i].position.X, scarf[i - 1].position.X - (int)(Math.Cos(scarf[i].angle) * length), lerpX);
                scarf[i].position.Y = MathHelper.Lerp(scarf[i].position.Y, scarf[i - 1].position.Y - (int)(Math.Sin(scarf[i].angle) * length - 1), lerpY);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 1; i < scarf2.Count; i++)
            {
                spriteBatch.Draw(scarfTexture, new Rectangle((int)scarf2[i].position.X, (int)scarf2[i].position.Y, (int)scarfSectionSize.X + i / 2 + 8, (int)scarfSectionSize.Y),
                    null, new Color(106, 156, 210), (float)scarf2[i].angle, new Vector2(5, 1), SpriteEffects.None, 0);
            }
            for (int i = 1; i < scarf1.Count; i++)
            {
                if (i == scarf1.Count - 1)
                {
                    spriteBatch.Draw(scarfEnd, new Rectangle((int)scarf1[i].position.X, (int)scarf1[i].position.Y, (int)scarfEnd.Width , (int)scarfEnd.Height),
                    null, Color.White, (float)scarf1[i].angle, new Vector2((int)scarfEnd.Width/2, 1), SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(scarfTexture, new Rectangle((int)scarf1[i].position.X, (int)scarf1[i].position.Y, (int)scarfSectionSize.X + i /2 + 8, (int)scarfSectionSize.Y),
                        null, new Color(140, 184, 233), (float)scarf1[i].angle, new Vector2(5, 1), SpriteEffects.None, 0);
                }
            }
        }
    }
}
