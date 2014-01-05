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
    class Section
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 prevPosition;
        public Vector2 velocity;
        public double angle;
        public float gravity;
        public float mass;
        public float radius = 8;
        public float stiffness = 0.2f;
        public float damping = 0.7f;

        public Section(Vector2 position, float mass, float gravity)
        {
            this.position = position;
            this.velocity.X = 0;
            this.velocity.Y = 0;
            this.mass = mass;
        }

        public void Update(float targetX, float targetY, float mass)
        {
            float forcex = (targetX - position.X) * stiffness;
            float ax = forcex / mass;
            velocity.X = damping * (velocity.X + ax);
            position.X += velocity.X;

            float forcey = ((targetY + radius) - position.Y) * stiffness;
            float ay = forcey / mass;
            velocity.Y = damping * (velocity.Y + ay);
            position.Y += velocity.Y;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            prevPosition = position;
        }
    }

    class VectorScarf : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private ContentManager content;
        private Game game;

        private bool frozen = false;

        BasicEffect basicEffect;
        List<VertexPositionColor> vertList;
        List<VertexPositionColor> vertList2;
        private Texture2D scarfEnd;

        private Vector2 base_Position;
        //VertexPositionColor[] vertices;

        List<Section> sections = new List<Section>();

        float gravity = 9.0f;
        float mass = 2.0f;


        public VectorScarf(Game game, Vector2 position)
            : base(game)
        {
            this.game = game;
            this.base_Position = position;
            basicEffect = new BasicEffect(game.GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
               (0, game.GraphicsDevice.Viewport.Width,     // left, right
                game.GraphicsDevice.Viewport.Height, 0,    // bottom, top
                0, 1);                                         // near, far plane

            vertList = new List<VertexPositionColor>();
            vertList2 = new List<VertexPositionColor>();

            //RasterizerState rs = new RasterizerState();
            //rs.CullMode = CullMode.None;
            //GraphicsDevice.RasterizerState = rs;

            // TODO: Add your initialization logic here
            NumOfSections(13);
        }

        public void NumOfSections(int num)
        {
            for (int i = 0; i < num; i++)
            {
                sections.Add(new Section(base_Position, mass, gravity));
                //sections[i].stiffness -= 1 / 10;
            }
        }

        public void LoadContent(IServiceProvider serviceProvider, ContentManager content)
        {
            this.content = content;
            scarfEnd = content.Load<Texture2D>("Scarf/scarfEnd");
        }

        public void Update(GameTime gameTime, Vector2 position)
        {

            this.base_Position = position;

            sections[0].Update(base_Position.X, base_Position.Y, sections[0].mass);
            sections[1].Update(sections[0].position.X, sections[0].position.Y, sections[1].mass);
            for (int i = 2; i < sections.Count; i++)
            {
                //sections[i - 1].Update(sections[i - 2].position.X, sections[i - 2].position.Y, sections[i - 1].mass);
                sections[i].Update(sections[i - 1].position.X, sections[i - 1].position.Y, sections[i].mass);
                if (i == sections.Count - 1)
                {
                    sections[i].Update(sections[i - 1].position.X, sections[i - 1].position.Y, sections[i].mass);
                }
            }


            vertList.Clear();
            vertList2.Clear();
            int j = 0;
            foreach (Section s in sections)
            {
                j++;
                float adjustAngle = s.velocity.X * 2;
                if (adjustAngle > s.radius / 2)
                {
                    adjustAngle = s.radius / 2;
                }
                else if (adjustAngle < -s.radius / 2)
                {
                    adjustAngle = -s.radius / 2;
                }
                vertList.Add(new VertexPositionColor(new Vector3(s.position.X - s.radius / 2 - j, s.position.Y - adjustAngle, 0), Color.SkyBlue));
                vertList.Add(new VertexPositionColor(new Vector3(s.position.X + s.radius / 2 + j, s.position.Y + adjustAngle, 0), Color.SkyBlue));
                vertList2.Add(new VertexPositionColor(new Vector3(s.position.X - s.radius / 2 - j, s.position.Y - adjustAngle, 0), Color.Red));
                vertList2.Add(new VertexPositionColor(new Vector3(s.position.X + s.radius / 2 + j, s.position.Y + adjustAngle, 0), Color.Red));
            }

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (this.frozen) return;

            //basicEffect.CurrentTechnique.Passes[0].Apply();

            game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertList.ToArray(), 0, sections.Count * 2 - 2);
            //game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertList2.ToArray(), 0, sections.Count - 1);

            spriteBatch.Draw(scarfEnd, sections[sections.Count - 1].position, Color.White); 
            base.Draw(gameTime);
        }

        // Move all the positions to relative and block the update loop
        public void FreezeScarf(Vector2 playerPosition)
        {
            this.frozen = true;
            for (int i = 0; i < sections.Count; i++)
            {
                sections[i].position.X -= playerPosition.X;
                sections[i].position.Y -= playerPosition.Y;
            }
        }

        // Move the positions back to absoloute based on new player position, and unblock updates
        public void UnfreezeScarf(Vector2 playerPosition)
        {
            for (int i = 0; i < sections.Count; i++)
            {
                sections[i].position.X += playerPosition.X;
                sections[i].position.Y += playerPosition.Y;
            }
            this.frozen = false;
        }
    }
}
