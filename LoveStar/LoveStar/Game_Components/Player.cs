using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LoveStar.Game_Components
{
    class Player
    {
        // Animations
        private Base_Components.Animation idleAnimation;
        private Base_Components.Animation walkAnimation;
        private Base_Components.Animation runAnimation;
        private Base_Components.Animation jumpAnimation;
        //private Base_Components.Animation fallAnimation;
        //private Base_Components.Animation crouchAnimation;
        private Base_Components.Animation crouch_IdleAnimation;
        private Base_Components.Animation crawlingAnimation;
        //private Base_Components.Animation talkAnimation;
        public SpriteEffects flip;
        private Base_Components.AnimationPlayer sprite = new Base_Components.AnimationPlayer();


        // Methods
        public Game_Mode Game_Mode
        {
            get { return game_Mode; }
        }
        Game_Mode game_Mode;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;

        Vector2 velocity;
        

        // Constants
        private const int width = 100;
        private const int height = 200;
        private const int boundingBoxWidthStanding = 40;
        private const int boundingBoxHeightStanding = 170;
        private const int boundingBoxHeightCrawling = 70;
        private const int boundingBoxOffsetX = 80;
        private const int boundingBoxOffsetYStanding = 30;
        private const int boundingBoxOffsetYCrawling = 130;

        // Calculate bounds within textures size;
        int boundingBoxLeft = boundingBoxOffsetX;
        int boundingBoxTop = boundingBoxOffsetYStanding;
        int boundingBoxWidth = boundingBoxWidthStanding;
        int boundingBoxHeight = boundingBoxHeightStanding;



        // Horizontal movement
        private const float noSpeed = 0.0f;
        private const float walkSpeed = 0.6f;
        private const float runSpeed = 1.7f;
        private const float crawlSpeed = 0.3f;

        private const float moveAcceleration = 14000.0f;
        private const float maxMoveSpeed = 2000.0f;
        private const float groundDragFactor = 0.58f;
        private const float airDragFactor = 0.65f;

        // Vertical movement
        private const float JumpVelocity = -5000.0f;
        private const float Gravity = 3500.0f;
        private const float MaxFallSpeed = 1000.0f;
        private const float JumpControlPower = 0.14f;
        private const float maxJumpTime = 40.0f;

        // Varibles
        private float movement;
        private bool Crawl_Toggle;
        private bool Crawl_Block;
        public bool isOnGround;
        private bool isRun;
        private bool isJumping;
        private bool wasJumping;
        private float jumpTime;
        private float previousBottom;
        private string outfit_Set;

        private Rectangle localBounds;

        // Gets a rectangle which bounds this player in world space.
        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(position.X - sprite.Origin.X) + localBounds.X;
                int top = (int)Math.Round(position.Y - sprite.Origin.Y) + localBounds.Y;

                return new Rectangle(left, top, localBounds.Width, localBounds.Height);
            }
        }

        public Player(Game_Mode game_Mode, Vector2 position, string outfit_Set)
        {
            this.game_Mode = game_Mode;

            Change_Outfit(outfit_Set);
            LoadContent();

            Reset(position);
        }

        public void Change_Outfit(string outfit_Set)
        {
            this.outfit_Set = outfit_Set;
            LoadContent();
        }

        public void animation_To_Idle()
        {
            sprite.PlayAnimation(idleAnimation);
        }

        public void LoadContent()
        {
            // Load animated textures.
            idleAnimation = new Base_Components.Animation(Game_Mode.Content.Load<Texture2D>("Sprites/Player/" + outfit_Set + "/Idle"), 0.2f, true);
            walkAnimation = new Base_Components.Animation(Game_Mode.Content.Load<Texture2D>("Sprites/Player/" + outfit_Set + "/Walking"), 0.11f, true);
            jumpAnimation = new Base_Components.Animation(Game_Mode.Content.Load<Texture2D>("Sprites/Player/" + outfit_Set + "/Jumping"), 0.08f, false);
            crawlingAnimation = new Base_Components.Animation(Game_Mode.Content.Load<Texture2D>("Sprites/Player/" + outfit_Set + "/Crawling"), 0.1f, true);
            runAnimation = new Base_Components.Animation(Game_Mode.Content.Load<Texture2D>("Sprites/Player/" + outfit_Set + "/Running"), 0.12f, true);
            crouch_IdleAnimation = new Base_Components.Animation(Game_Mode.Content.Load<Texture2D>("Sprites/Player/" + outfit_Set + "/Crouch_Idle"), 0.12f, true);
        }

        public void Reset(Vector2 position)
        {
            Position = position;
            isOnGround = false;
            wasJumping = false;
            isJumping = false;
            jumpTime = 0.0f;
            Crawl_Toggle = false;
            velocity = Vector2.Zero;
            sprite.PlayAnimation(idleAnimation);
        }

        public void Update(GameTime gameTime, KeyPress keypress, bool halt_movement)
        {
            // Calculate bounds within texture size.
            localBounds = new Rectangle(boundingBoxLeft, boundingBoxTop, boundingBoxWidth, boundingBoxHeight);
            if (halt_movement == false)
            {
                GetInput(keypress);
            }
            ApplyPhysics(gameTime);

            if (isOnGround)
            {
                if (Math.Abs(velocity.X) /*- 0.02f*/ > 0)
                {
                    if (isRun)
                    {
                        sprite.PlayAnimation(runAnimation);
                    }

                    else if (Crawl_Toggle)
                    {
                        sprite.PlayAnimation(crawlingAnimation);
                    }

                    else
                    {
                        sprite.PlayAnimation(walkAnimation);
                    }
                }
                else if (movement == noSpeed)
                {
                    if (Crawl_Toggle)
                    {
                        sprite.PlayAnimation(crouch_IdleAnimation);
                    }

                    else
                    {
                        sprite.PlayAnimation(idleAnimation);
                    }
                }
                else
                {
                    sprite.PlayAnimation(idleAnimation);
                }
            }

            // Clear input.
            movement = noSpeed;
            isJumping = false;
        }

        private void GetInput(KeyPress keypress)
        {
            // Gamepad

            // Keyboard

            if (keypress.key_C == 1 && isOnGround)
            {
                if (Crawl_Toggle == true)
                {
                    if (Crawl_Block != true)
                    {
                        Crawl_Toggle = false;
                    }
                }
                else if (Crawl_Toggle == false)
                {
                    Crawl_Toggle = true;
                }

            }


            ///<If both left and right are pressed, there is no horizontal movement>
            if (keypress.key_Right == 1 && keypress.key_Left == 1)
            {
                movement = noSpeed;
            }

            ///<Left Movement>
            ///<If left is pressed, there is horizontal movement>
            else if (keypress.key_Left >= 1)
            {
                if (Crawl_Toggle == true)
                {
                    movement = -crawlSpeed;
                    isRun = false;
                }
                ///<If shift is also pressed, horizontal movement will be at runSpeed>
                else if (keypress.key_Shift >= 1)
                {
                    movement = -runSpeed;
                    isRun = true;
                }
                ///<If not, the horizontal movement will be at walkspeed>
                else
                {
                    movement = -walkSpeed;
                    isRun = false;
                }
            }
            ///<Right Movement>
            ///<If right is pressed, there is horizontal movement>
            else if (keypress.key_Right >= 1)
            {
                if (Crawl_Toggle == true)
                {
                    movement = crawlSpeed;
                    isRun = false;
                }
                ///<If shift is also pressed, horizontal movement will be at runSpeed>
                else if (keypress.key_Shift >= 1)
                {
                    movement = runSpeed;
                    isRun = true;
                }
                ///<If not, the horizontal movement will be at walkspeed>
                else
                {
                    movement = walkSpeed;
                    isRun = false;
                }
            }
            else if (keypress.key_Right == 0 && keypress.key_Left == 0)
            {
                velocity.X = noSpeed;
                isRun = false;
            }

            if (keypress.key_Space >= 1 || keypress.key_Z >= 1 || keypress.key_Up >= 1)
            {
                isJumping = true;
            }
        }

        public void ApplyPhysics(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 previousPosition = position;



            // Base velocity is a combination of horizontal movement control and
            // acceleration downward due to gravity.
            velocity.X += movement * moveAcceleration * elapsed;
            velocity.Y = MathHelper.Clamp(velocity.Y + Gravity * elapsed, -MaxFallSpeed, MaxFallSpeed);

            velocity.Y = DoJump(velocity.Y, gameTime);

            // Apply pseudo-drag horizontally.
            if (isOnGround)
                velocity.X *= groundDragFactor;
            else
                velocity.X *= airDragFactor;

            // Prevent the player from running faster than his top speed.            
            velocity.X = MathHelper.Clamp(velocity.X, -maxMoveSpeed, maxMoveSpeed);

            // Apply velocity.
            position += velocity * elapsed;
            position = new Vector2((float)Math.Round(position.X), (float)Math.Round(position.Y));

            // If the player is now colliding with the level, separate them.
            HandleCollisions();

            // If the collision stopped us from moving, reset the velocity to zero.
            if (position.X == previousPosition.X)
                movement = noSpeed;

            if (position.Y == previousPosition.Y)
                velocity.Y = 0;
        }

        private float DoJump(float velocityY, GameTime gameTime)
        {
            // If the player wants to jump
            if (isJumping)
            {

                // Begin or continue a jump
                if ((!wasJumping && isOnGround))
                {
                    jumpTime = 20.0f;
                    sprite.PlayAnimation(jumpAnimation);
                }


                // If we are in the ascent of the jump
                if (0.0f < jumpTime && jumpTime <= maxJumpTime)
                {
                    // Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
                    velocityY = JumpVelocity * (1.0f - (float)Math.Pow(jumpTime / maxJumpTime, JumpControlPower));
                    jumpTime -= 5.0f;
                }
                else
                {
                    // Reached the apex of the jump
                    jumpTime = 0.0f;
                }
            }
            else
            {
                // Continues not jumping or cancels a jump in progress
                jumpTime = 0.0f;
            }
            wasJumping = isJumping;

            return velocityY;
        }

        private void HandleCollisions()
        {
            if (Crawl_Toggle == false)
            {
                boundingBoxLeft = boundingBoxOffsetX;
                boundingBoxTop = boundingBoxOffsetYStanding;
                boundingBoxWidth = boundingBoxWidthStanding;
                boundingBoxHeight = boundingBoxHeightStanding;
            }
            if (Crawl_Toggle == true)
            {
                boundingBoxLeft = boundingBoxOffsetX;
                boundingBoxTop = boundingBoxOffsetYCrawling;
                boundingBoxWidth = boundingBoxWidthStanding;
                boundingBoxHeight = boundingBoxHeightCrawling;
                wasJumping = true;
                ///<To Do>
                ///<make a new width for the crawling when the sprite is made>
            }

            // Get the player's bounding rectangle and find neighboring tiles.
            Rectangle bounds = BoundingRectangle;
            int leftTile = (int)Math.Floor((float)bounds.Left / Tiles.Width);
            int rightTile = (int)Math.Ceiling(((float)bounds.Right / Tiles.Width)) - 1;
            int topTile = (int)Math.Floor((float)bounds.Top / Tiles.Height);
            int bottomTile = (int)Math.Ceiling(((float)bounds.Bottom / Tiles.Height)) - 1;

            // Reset flag to search for ground collision.
            isOnGround = false;

            // For each potentially colliding tile,
            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    // If this tile is collidable,
                    TileType collision = game_Mode.GetCollision(x, y);
                    if (collision != TileType.Passable)
                    {
                        // Determine collision depth (with direction) and magnitude.
                        Rectangle tileBounds = game_Mode.GetBounds(x, y);
                        Vector2 depth = Base_Components.RectangleExtensions.GetIntersectionDepth(bounds, tileBounds);
                        if (depth != Vector2.Zero)
                        {
                            float absDepthX = Math.Abs(depth.X);
                            float absDepthY = Math.Abs(depth.Y);

                            // Resolve the collision along the shallow axis.
                            if (absDepthY < absDepthX || collision == TileType.Platform)
                            {
                                // If we crossed the top of a tile, we are on the ground.
                                if (previousBottom <= tileBounds.Top)
                                    isOnGround = true;

                                // Ignore platforms, unless we are on the ground.
                                if (collision == TileType.Impassable || isOnGround)
                                {
                                    // Resolve the collision along the Y axis.
                                    Position = new Vector2(Position.X, Position.Y + depth.Y);

                                    // Perform further collisions with the new bounds.
                                    bounds = BoundingRectangle;
                                }
                            }
                            else if (collision == TileType.Impassable) // Ignore platforms.
                            {
                                // Resolve the collision along the X axis
                                Position = new Vector2(Position.X + depth.X, Position.Y);

                                // Perform further collisions with the new bounds.
                                bounds = BoundingRectangle;
                            }
                        }
    
                    }
                    TileAction crawl_Collision = game_Mode.GetCrawl(x, y - 1);
                    if (crawl_Collision == TileAction.Crawl)
                    {
                        Crawl_Block = true;
                    }
                    else
                    {
                        Crawl_Block = false;
                    }
                }
            }

            // Save the new bounds bottom.
            previousBottom = bounds.Bottom;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Flip the sprite to face the way we are moving.
            if (velocity.X > 0)
            {
                flip = SpriteEffects.FlipHorizontally; 
            }
            else if (velocity.X < 0)
            {
                flip = SpriteEffects.None;
            }
            // Draw that sprite.
            sprite.Draw(gameTime, spriteBatch, Position, flip,0);
        }
    }
}
