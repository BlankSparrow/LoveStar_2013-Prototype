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
            scarfTexture = content.Load<Texture2D>("Scarf/Section");
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            this.position = position;

            updateScarf(gameTime, position, scarf1, 0.5f, 0.6f);
            updateScarf(gameTime, position, scarf2, 0.6f, 0.51f);
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
                    scarf[i - 1].position.X - scarf[i].position.X);

                scarf[i].position.X = MathHelper.Lerp(scarf[i].position.X, scarf[i - 1].position.X - (int)(Math.Cos(scarf[i].angle) * length), lerpX);
                scarf[i].position.Y = MathHelper.Lerp(scarf[i].position.Y, scarf[i - 1].position.Y - (int)(Math.Sin(scarf[i].angle) * length - 1), lerpY);
            }
        }

        public void DrawBack(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (section s in scarf2)
            {
                spriteBatch.Draw(scarfTexture, new Rectangle((int)s.position.X, (int)s.position.Y, scarfTexture.Width, scarfTexture.Height), null, new Color(0, 110, 250), (float)s.angle + 1.5f, new Vector2(10, 10), SpriteEffects.None, 0);
            }
        }
        public void DrawFront(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (section s in scarf1)
            {
                spriteBatch.Draw(scarfTexture, new Rectangle((int)s.position.X, (int)s.position.Y, scarfTexture.Width, scarfTexture.Height), null, new Color(30, 140, 256), (float)s.angle + 1.5f, new Vector2(10, 10), SpriteEffects.None, 0);
            }
        }

    }
}
