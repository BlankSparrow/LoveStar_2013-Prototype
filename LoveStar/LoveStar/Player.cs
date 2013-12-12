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

    class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {
        /// <summary>
        /// Switch for what state of animation the player is in
        /// </summary>
        enum Action_State
        {
            stand,
            stance,
            walk,
            run,
        }

        /// <summary>
        /// Switch for which direction the player
        /// </summary>
        enum Facing_Direction
        {
            left,
            right,
        }

        /// <summary>
        /// Switch for what foot should come next after standing
        /// </summary>
        enum Next_Step
        {
            left,
            right,
        }
        
        ContentManager content;
        Action_State actionState;
        Facing_Direction facingDirection;
        Next_Step nextStep;


        private Vector2 levelSize;
        private bool canMoveRight;
        private bool canMoveLeft;

        private Game game;
        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; } }

        const float frameRate = 0.15f;
        private int prevFrame;
        private Tools.Animation prevAni;

        private Tools.AnimationPlayer sprite = new Tools.AnimationPlayer();

        // Right
        // Stand
        private Tools.Animation RStand;
        private Tools.Animation RStand_LStand;
        private Tools.Animation RStand_RStance;
        private Tools.Animation RStand_RRStep;
        private Tools.Animation RStand_RLStep;
        // Stance
        private Tools.Animation RStance;
        private Tools.Animation RStance_LStance;
        private Tools.Animation RStance_RStand;
        private Tools.Animation RStance_RRRun;
        private Tools.Animation RStance_RLRun;
        // Walk
        private Tools.Animation RRStep_RStand;
        private Tools.Animation RLStep_RStand;
        private Tools.Animation RRStep_RLStep;
        private Tools.Animation RLStep_RRStep;
        private Tools.Animation RRStep_RLRun;
        private Tools.Animation RLStep_RRRun;
        // Run
        private Tools.Animation RRRun_RStance;
        private Tools.Animation RLRun_RStance;
        private Tools.Animation RRRun_RLRun;
        private Tools.Animation RLRun_RRRun;
        private Tools.Animation RRRun_RLStep;
        private Tools.Animation RLRun_RRStep;

        // Left
        // Stand
        private Tools.Animation LStand;
        private Tools.Animation LStand_RStand;
        private Tools.Animation LStand_LStance;
        private Tools.Animation LStand_LRStep;
        private Tools.Animation LStand_LLStep;
        // Stance
        private Tools.Animation LStance;
        private Tools.Animation LStance_RStance;
        private Tools.Animation LStance_LStand;
        private Tools.Animation LStance_LRRun;
        private Tools.Animation LStance_LLRun;
        // Walk
        private Tools.Animation LRStep_LStand;
        private Tools.Animation LLStep_LStand;
        private Tools.Animation LRStep_LLStep;
        private Tools.Animation LLStep_LRStep;
        private Tools.Animation LRStep_LLRun;
        private Tools.Animation LLStep_LRRun;
        // Run
        private Tools.Animation LRRun_LStance;
        private Tools.Animation LLRun_LStance;
        private Tools.Animation LRRun_LLRun;
        private Tools.Animation LLRun_LRRun;
        private Tools.Animation LRRun_LLStep;
        private Tools.Animation LLRun_LRStep;

        private LoveStar.Scarf scarf;
        private Vector2 scarfPosition;

        public void setCanMove(bool canMoveRight, bool canMoveLeft)
        {
            this.canMoveRight = canMoveRight;
            this.canMoveLeft = canMoveLeft;
        }

        public void setLevelSize(Vector2 levelSize)
        {
            this.levelSize = levelSize;
        }

        public Player(Game game, GraphicsDeviceManager graphics)
            : base(game)
        {
            this.game = game;
            position = new Vector2(400, 410);
            actionState = Action_State.stand;
            facingDirection = Facing_Direction.right;
            nextStep = Next_Step.right;
            scarfPosition = position - new Vector2(0, -180);

            scarf = new Scarf(game, position);
            setCanMove(true,true);
        }

        public void LoadContent(IServiceProvider serviceProvider, ContentManager content)
        {
            this.content = content;
            LoadSprite(serviceProvider, content, "Adv_Proto");
            sprite.PlayAnimation(AniSwitch(RStand, LStand));
            scarf.LoadContent(serviceProvider, content);
        }

        private void LoadSprite(IServiceProvider serviceProvider, ContentManager content, string textureSet)
        {
            // Right
            // Stand
            RStand = LoadSpriteFile(content, textureSet, "Right", "RStand");
            RStand_LStand = LoadSpriteFile(content, textureSet, "Right", "RStand_LStand");
            RStand_RStance = LoadSpriteFile(content, textureSet, "Right", "RStand_RStance");
            RStand_RRStep = LoadSpriteFile(content, textureSet, "Right", "RStand_RRStep");
            RStand_RLStep = LoadSpriteFile(content, textureSet, "Right", "RStand_RLStep");
            // Stance
            RStance = LoadSpriteFile(content, textureSet, "Right", "RStance");
            RStance_LStance = LoadSpriteFile(content, textureSet, "Right", "RStance_LStance");
            RStance_RStand = LoadSpriteFile(content, textureSet, "Right", "RStance_RStand");
            RStance_RRRun = LoadSpriteFile(content, textureSet, "Right", "RStance_RRRun");
            RStance_RLRun = LoadSpriteFile(content, textureSet, "Right", "RStance_RLRun");
            // Walk
            RRStep_RStand = LoadSpriteFile(content, textureSet, "Right", "RRStep_RStand");
            RLStep_RStand = LoadSpriteFile(content, textureSet, "Right", "RLStep_RStand");
            RRStep_RLStep = LoadSpriteFile(content, textureSet, "Right", "RRStep_RLStep");
            RLStep_RRStep = LoadSpriteFile(content, textureSet, "Right", "RLStep_RRStep");
            RRStep_RLRun = LoadSpriteFile(content, textureSet, "Right", "RRStep_RLRun");
            RLStep_RRRun = LoadSpriteFile(content, textureSet, "Right", "RLStep_RRRun");
            // Run
            RRRun_RStance = LoadSpriteFile(content, textureSet, "Right", "RRRun_RStance");
            RLRun_RStance = LoadSpriteFile(content, textureSet, "Right", "RLRun_RStance");
            RRRun_RLRun = LoadSpriteFile(content, textureSet, "Right", "RRRun_RLRun");
            RLRun_RRRun = LoadSpriteFile(content, textureSet, "Right", "RLRun_RRRun");
            RRRun_RLStep = LoadSpriteFile(content, textureSet, "Right", "RLRun_RRRun");
            RLRun_RRStep = LoadSpriteFile(content, textureSet, "Right", "RLRun_RRStep");

            // Left
            // Stand
            LStand = LoadSpriteFile(content, textureSet, "Left", "LStand");
            LStand_RStand = LoadSpriteFile(content, textureSet, "Left", "LStand_RStand");
            LStand_LStance = LoadSpriteFile(content, textureSet, "Left", "LStand_LStance");
            LStand_LRStep = LoadSpriteFile(content, textureSet, "Left", "LStand_LRStep");
            LStand_LLStep = LoadSpriteFile(content, textureSet, "Left", "LStand_LLStep");
            // Stance
            LStance = LoadSpriteFile(content, textureSet, "Left", "LStance");
            LStance_RStance = LoadSpriteFile(content, textureSet, "Left", "LStance_RStance");
            LStance_LStand = LoadSpriteFile(content, textureSet, "Left", "LStance_LStand");
            LStance_LRRun = LoadSpriteFile(content, textureSet, "Left", "LStance_LRRun");
            LStance_LLRun = LoadSpriteFile(content, textureSet, "Left", "LStance_LLRun");
            // Walk
            LRStep_LStand = LoadSpriteFile(content, textureSet, "Left", "LRStep_LStand");
            LLStep_LStand = LoadSpriteFile(content, textureSet, "Left", "LLStep_LStand");
            LRStep_LLStep = LoadSpriteFile(content, textureSet, "Left", "LRStep_LLStep");
            LLStep_LRStep = LoadSpriteFile(content, textureSet, "Left", "LLStep_LRStep");
            LRStep_LLRun = LoadSpriteFile(content, textureSet, "Left", "LRStep_LLRun");
            LLStep_LRRun = LoadSpriteFile(content, textureSet, "Left", "LLStep_LRRun");
            // Run
            LRRun_LStance = LoadSpriteFile(content, textureSet, "Left", "LRRun_LStance");
            LLRun_LStance = LoadSpriteFile(content, textureSet, "Left", "LLRun_LStance");
            LRRun_LLRun = LoadSpriteFile(content, textureSet, "Left", "LRRun_LLRun");
            LLRun_LRRun = LoadSpriteFile(content, textureSet, "Left", "LLRun_LRRun");
            LRRun_LLStep = LoadSpriteFile(content, textureSet, "Left", "LRRun_LLStep");
            LLRun_LRStep = LoadSpriteFile(content, textureSet, "Left", "LLRun_LRStep");
        }

        private Tools.Animation LoadSpriteFile(ContentManager content, string textureSet, string direction, string spriteFile)
        {
            return LoadSpriteFile(content, textureSet, direction, spriteFile, true);
        }

        private Tools.Animation LoadSpriteFile(ContentManager content, string textureSet, string direction, string spriteFile, bool repeatAnimation)
        {
            return new Tools.Animation(content.Load<Texture2D>("GameMode/Player/" + textureSet + "/" + direction + "/" + spriteFile), frameRate, repeatAnimation);
        }

        public Player Update(GameTime gameTime, Tools.KeyPress keyPress, Player Player)
        {
            Proto_Movement(gameTime, keyPress);
            FrameBasedMovement(gameTime);

            return Player;
        }
        /*
         * Working Here Onwards on a better Movement Class
         * -
         * Note:
         * An idea is to check every new frame and then use that as a base to move from
         * */
        private void FrameBasedMovement(GameTime gameTime)
        {
            if (sprite.FrameIndex != prevFrame || sprite.Animation != prevAni)
            {
                // stand to step
                test_Movement(gameTime, RStand_RRStep, LStand_LLStep, 0, 2, new Vector2(0, -190));
                test_Movement(gameTime, RStand_RLStep, LStand_LRStep, 0, 10, new Vector2(0, -190));

                // step
                test_Movement(gameTime, RRStep_RLStep, LLStep_LRStep, 0, 18, new Vector2(0, -190));
                test_Movement(gameTime, RLStep_RRStep, LRStep_LLStep, 0, 18, new Vector2(0, -190));
                test_Movement(gameTime, RRStep_RLStep, LLStep_LRStep, 1, 18, new Vector2(0, -190));
                test_Movement(gameTime, RLStep_RRStep, LRStep_LLStep, 1, 18, new Vector2(0, -190));
                test_Movement(gameTime, RRStep_RLStep, LLStep_LRStep, 2, 18, new Vector2(0, -190));
                test_Movement(gameTime, RLStep_RRStep, LRStep_LLStep, 2, 18, new Vector2(0, -190));

                // step to stand
                test_Movement(gameTime, RRStep_RStand, LLStep_LStand, 0, 6, new Vector2(0, -190));
                test_Movement(gameTime, RLStep_RStand, LRStep_LStand, 0, 6, new Vector2(0, -190));
                test_Movement(gameTime, RRStep_RStand, LLStep_LStand, 1, 6, new Vector2(0, -190));
                test_Movement(gameTime, RLStep_RStand, LRStep_LStand, 1, 6, new Vector2(0, -190));

                // stance to run
                test_Movement(gameTime, RStance_RRRun, LStance_LLRun, 0, 19, new Vector2(0, -190));
                test_Movement(gameTime, RStance_RLRun, LStance_LRRun, 0, 19, new Vector2(0, -190));
                test_Movement(gameTime, RStance_RRRun, LStance_LLRun, 1, 19, new Vector2(0, -190));
                test_Movement(gameTime, RStance_RLRun, LStance_LRRun, 1, 19, new Vector2(0, -190));

                // run
                test_Movement(gameTime, RRRun_RLRun, LLRun_LRRun, 0, 50, new Vector2(0, -190));
                test_Movement(gameTime, RLRun_RRRun, LRRun_LLRun, 0, 50, new Vector2(0, -190));
                test_Movement(gameTime, RRRun_RLRun, LLRun_LRRun, 1, 50, new Vector2(0, -190));
                test_Movement(gameTime, RLRun_RRRun, LRRun_LLRun, 1, 50, new Vector2(0, -190));
                test_Movement(gameTime, RRRun_RLRun, LLRun_LRRun, 2, 50, new Vector2(0, -190));
                test_Movement(gameTime, RLRun_RRRun, LRRun_LLRun, 2, 50, new Vector2(0, -190));

                // run to stance
                test_Movement(gameTime, RRRun_RStance, LLRun_LStance, 0, 14, new Vector2(0, -190));
                test_Movement(gameTime, RLRun_RStance, LRRun_LStance, 0, 14, new Vector2(0, -190));
                test_Movement(gameTime, RRRun_RStance, LLRun_LStance, 1, 14, new Vector2(0, -190));
                test_Movement(gameTime, RLRun_RStance, LRRun_LStance, 1, 14, new Vector2(0, -190));

                // step to run
                test_Movement(gameTime, RRStep_RLRun, LLStep_LRRun, 0, 12, new Vector2(0, -190));
                test_Movement(gameTime, RLStep_RRRun, LRStep_LLRun, 0, 12, new Vector2(0, -190));
                test_Movement(gameTime, RRStep_RLRun, LLStep_LRRun, 1, 12, new Vector2(0, -190));
                test_Movement(gameTime, RLStep_RRRun, LRStep_LLRun, 1, 12, new Vector2(0, -190));
                test_Movement(gameTime, RRStep_RLRun, LLStep_LRRun, 2, 12, new Vector2(0, -190));
                test_Movement(gameTime, RLStep_RRRun, LRStep_LLRun, 2, 12, new Vector2(0, -190));

                // run to step
                test_Movement(gameTime, RRRun_RLStep, LLRun_LRStep, 0, 10, new Vector2(0, -190));
                test_Movement(gameTime, RLRun_RRStep, LRRun_LLStep, 0, 10, new Vector2(0, -190));
                test_Movement(gameTime, RRRun_RLStep, LLRun_LRStep, 1, 10, new Vector2(0, -190));
                test_Movement(gameTime, RLRun_RRStep, LRRun_LLStep, 1, 10, new Vector2(0, -190));
                test_Movement(gameTime, RRRun_RLStep, LLRun_LRStep, 2, 10, new Vector2(0, -190));
                test_Movement(gameTime, RLRun_RRStep, LRRun_LLStep, 2, 10, new Vector2(0, -190));
            }
            prevFrame = sprite.FrameIndex;
            prevAni = sprite.Animation;
        }



        // being used instead og contextualMovement due to sprites being mirrored at thi point
        private void test_Movement(GameTime gameTime, Tools.Animation RightAni, Tools.Animation LeftAni, int frame, int x_positionChange, Vector2 scarfOffset)
        {
            if (frame == sprite.FrameIndex)
            {
                if (sprite.Animation == RightAni)
                {
                    position.X += x_positionChange;
                }
                else if (sprite.Animation == LeftAni)
                {
                    position.X -= x_positionChange;
                }
                scarfPosition = position + scarfOffset;
                scarf.Update(gameTime, scarfPosition);
            }
            
        }

        private void Proto_Movement(GameTime gameTime, Tools.KeyPress keyPress)
        {
            switch (actionState)
            {
                case Action_State.stand:
                    if (sprite.Animation == RStand) { facingDirection = Facing_Direction.right; }
                    if (sprite.Animation == LStand) { facingDirection = Facing_Direction.left; }

                    if (sprite.Animation == RStand || sprite.Animation == LStand)
                    {
                        if (sprite.FrameIndex == 1)
                        {
                            if (keyPress.key_Right >= 1 && keyPress.key_Left >= 1)
                            {
                                sprite.PlayAnimation(AniSwitch(RStand, LStand));
                            }
                            else if (keyPress.key_Z >= 1)
                            {
                                sprite.PlayAnimation(AniSwitch(RStand_RStance, LStand_LStance));
                            }
                            else if (keyPress.key_Right >= 1)
                            {
                                if (facingDirection == Facing_Direction.left) { sprite.PlayAnimation(LStand_RStand); }
                                else
                                {
                                    sprite.PlayAnimation(AniSwitchStep(RStand_RRStep, RStand_RLStep));
                                }
                            }
                            else if (keyPress.key_Left >= 1)
                            {
                                if (facingDirection == Facing_Direction.right) { sprite.PlayAnimation(RStand_LStand); }
                                else
                                {
                                    sprite.PlayAnimation(AniSwitchStep(LStand_LRStep, LStand_LLStep));
                                }
                            }
                        }
                    }
                    else if (sprite.Animation == RStand_LStand || sprite.Animation == LStand_RStand)
                    {
                        if (sprite.FrameIndex == 2)
                        {
                            sprite.PlayAnimation(AniSwitch(LStand, RStand));
                        }
                    }
                    else if (sprite.Animation == RStand_RStance || sprite.Animation == LStand_LStance)
                    {
                        if (sprite.FrameIndex == 1)
                        {
                            sprite.PlayAnimation(AniSwitch(RStance, LStance));
                            actionState = Action_State.stance;
                        }
                    }
                    else if (sprite.Animation == RStand_RRStep || sprite.Animation == RStand_RLStep ||
                        sprite.Animation == LStand_LRStep || sprite.Animation == LStand_LLStep)
                    {
                        if (sprite.FrameIndex == 1)
                        {
                            if (keyPress.key_Right >= 1 && facingDirection == Facing_Direction.right)
                            {
                                if (sprite.Animation == RStand_RRStep || sprite.Animation == RStand_RLStep)
                                {
                                    sprite.PlayAnimation(AniSwitchStep(RRStep_RLStep, RLStep_RRStep));
                                    actionState = Action_State.walk;
                                }
                            }
                            else if (keyPress.key_Left >= 1 && facingDirection == Facing_Direction.left)
                            {
                                if (sprite.Animation == LStand_LRStep || sprite.Animation == LStand_LLStep)
                                {
                                    sprite.PlayAnimation(AniSwitchStep(LRStep_LLStep, LLStep_LRStep));
                                    actionState = Action_State.walk;
                                }
                            }
                            else
                            {
                                sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRStep_RStand, RLStep_RStand), AniSwitchStep(LRStep_LStand, LLStep_LStand)));
                            }
                        }
                    }
                    else if (sprite.Animation == RRStep_RStand || sprite.Animation == RLStep_RStand ||
                         sprite.Animation == LRStep_LStand || sprite.Animation == LLStep_LStand)
                    {
                        if (sprite.FrameIndex == 2)
                        {
                            if (sprite.Animation == RRStep_RStand || sprite.Animation == LRStep_LStand)
                            {
                                nextStep = Next_Step.left;
                                sprite.PlayAnimation(AniSwitch(RStand, LStand));
                            }
                            else if (sprite.Animation == RLStep_RStand || sprite.Animation == LLStep_LStand)
                            {
                                nextStep = Next_Step.right;
                                sprite.PlayAnimation(AniSwitch(RStand, LStand));
                            }
                        }
                    }
                    else
                    {
                        sprite.PlayAnimation(AniSwitch(RStand, LStand));
                    }
                    break;
                case Action_State.walk:
                    if (sprite.Animation == RRStep_RLStep || sprite.Animation == RLStep_RRStep ||
                        sprite.Animation == LRStep_LLStep || sprite.Animation == LLStep_LRStep)
                    {
                        if (sprite.FrameIndex == 3)
                        {
                            if (keyPress.key_Right >= 1)
                            {
                                if (sprite.Animation == RRStep_RLStep)
                                {
                                    if (keyPress.key_Z >= 1)
                                    {
                                        sprite.PlayAnimation(RLStep_RRRun);
                                        nextStep = Next_Step.right;
                                    }
                                    else
                                    {
                                        sprite.PlayAnimation(RLStep_RRStep);
                                        nextStep = Next_Step.right;
                                    }
                                }
                                else if (sprite.Animation == RLStep_RRStep)
                                {
                                    if (keyPress.key_Z >= 1)
                                    {
                                        sprite.PlayAnimation(RRStep_RLRun);
                                        nextStep = Next_Step.left;
                                    }
                                    else
                                    {
                                        sprite.PlayAnimation(RRStep_RLStep);
                                        nextStep = Next_Step.left;
                                    }
                                }
                                else
                                {
                                    sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRStep_RStand, RLStep_RStand), AniSwitchStep(LRStep_LStand, LLStep_LStand)));
                                }
                            }
                            else if (keyPress.key_Left >= 1)
                            {
                                if (sprite.Animation == LRStep_LLStep)
                                {
                                    if (keyPress.key_Z >= 1)
                                    {
                                        sprite.PlayAnimation(LLStep_LRRun);
                                        nextStep = Next_Step.right;
                                    }
                                    else
                                    {
                                        sprite.PlayAnimation(LLStep_LRStep);
                                        nextStep = Next_Step.right;
                                    }
                                }
                                else if (sprite.Animation == LLStep_LRStep)
                                {
                                    if (keyPress.key_Z >= 1)
                                    {
                                        sprite.PlayAnimation(LRStep_LLRun);
                                        nextStep = Next_Step.left;
                                    }
                                    else
                                    {
                                        sprite.PlayAnimation(LRStep_LLStep);
                                        nextStep = Next_Step.left;
                                    }
                                }
                                else
                                {
                                    sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRStep_RStand, RLStep_RStand), AniSwitchStep(LRStep_LStand, LLStep_LStand)));
                                }
                            }
                            else
                            {
                                sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRStep_RStand, RLStep_RStand), AniSwitchStep(LRStep_LStand, LLStep_LStand)));
                            }
                        }
                    }
                    else if (sprite.Animation == RRStep_RLRun || sprite.Animation == RLStep_RRRun ||
                        sprite.Animation == LRStep_LLRun || sprite.Animation == LLStep_LRRun)
                    {
                        if (sprite.FrameIndex == 3)
                        {
                            sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRRun_RLRun, RLRun_RRRun), AniSwitchStep(LRRun_LLRun, LLRun_LRRun)));
                            actionState = Action_State.run;
                        }
                    }
                    else if (sprite.Animation == RRStep_RStand || sprite.Animation == RLStep_RStand ||
                        sprite.Animation == LRStep_LStand || sprite.Animation == LLStep_LStand)
                    {
                        if (sprite.FrameIndex == 2)
                        {
                            if (sprite.Animation == RRStep_RStand || sprite.Animation == LRStep_LStand)
                            {
                                nextStep = Next_Step.left;
                                sprite.PlayAnimation(AniSwitch(RStand, LStand));
                                actionState = Action_State.stand;
                            }
                            else if (sprite.Animation == RLStep_RStand || sprite.Animation == LLStep_LStand)
                            {
                                nextStep = Next_Step.right;
                                sprite.PlayAnimation(AniSwitch(RStand, LStand));
                                actionState = Action_State.stand;
                            }
                        }
                    }
                    else
                    {
                        if (sprite.FrameIndex == 3)
                        {
                            sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRStep_RStand, RLStep_RStand), AniSwitchStep(LRStep_LStand, LLStep_LStand)));
                        }
                    }
                    break;
                case Action_State.stance:
                    if (sprite.Animation == RStance) { facingDirection = Facing_Direction.right; }
                    if (sprite.Animation == LStance) { facingDirection = Facing_Direction.left; }

                    if (sprite.Animation == RStance || sprite.Animation == LStance)
                    {
                        if (sprite.FrameIndex == 1)
                        {
                            if (keyPress.key_Z >= 1)
                            {
                                if (facingDirection == Facing_Direction.right)
                                {
                                    if (keyPress.key_Left >= 1)
                                    {
                                        sprite.PlayAnimation(RStance_LStance);
                                    }
                                    else if (keyPress.key_Right >= 1)
                                    {
                                        sprite.PlayAnimation(AniSwitchStep(RStance_RRRun, RStance_RLRun));
                                    }
                                }
                                else if (facingDirection == Facing_Direction.left)
                                {
                                    if (keyPress.key_Right >= 1)
                                    {
                                        sprite.PlayAnimation(LStance_RStance);
                                    }
                                    else if (keyPress.key_Left >= 1)
                                    {
                                        sprite.PlayAnimation(AniSwitchStep(LStance_LRRun, LStance_LLRun));
                                    }
                                }
                            }
                            else
                            {
                                sprite.PlayAnimation(AniSwitch(RStance_RStand, LStance_LStand));
                            }
                        }
                    }
                    else if (sprite.Animation == RStance_RRRun || sprite.Animation == RStance_RLRun ||
                        sprite.Animation == LStance_LRRun || sprite.Animation == LStance_LLRun)
                    {
                        if (sprite.FrameIndex == 2)
                        {
                            if (facingDirection == Facing_Direction.right)
                            {
                                if (keyPress.key_Left >= 1)
                                {
                                    sprite.PlayAnimation(AniSwitchStep(RRRun_RStance, RLRun_RStance));
                                }
                                else if (keyPress.key_Right >= 1)
                                {
                                    sprite.PlayAnimation(AniSwitchStep(RRRun_RLRun, RLRun_RRRun));
                                    actionState = Action_State.run;
                                }
                                else
                                {
                                    sprite.PlayAnimation(AniSwitchStep(RRRun_RStance, RLRun_RStance));
                                }
                            }
                            else if (facingDirection == Facing_Direction.left)
                            {
                                if (keyPress.key_Right >= 1)
                                {
                                    sprite.PlayAnimation(AniSwitchStep(LRRun_LStance, LLRun_LStance));
                                }
                                else if (keyPress.key_Left >= 1)
                                {
                                    sprite.PlayAnimation(AniSwitchStep(LRRun_LLRun, LLRun_LRRun));
                                    actionState = Action_State.run;
                                }
                                else
                                {
                                    sprite.PlayAnimation(AniSwitchStep(LRRun_LStance, LLRun_LStance));
                                }
                            }
                        }
                    }
                    else if (sprite.Animation == RStance_RStand || sprite.Animation == LStance_LStand)
                    {
                        if (sprite.FrameIndex == 1)
                        {
                            sprite.PlayAnimation(AniSwitch(RStand, LStand));
                            actionState = Action_State.stand;
                        }
                    }
                    else if (sprite.Animation == RRRun_RStance || sprite.Animation == RLRun_RStance ||
                        sprite.Animation == LRRun_LStance || sprite.Animation == LLRun_LStance)
                    {
                        if (sprite.FrameIndex == 2)
                        {
                            if (sprite.Animation == RRRun_RStance)
                            {
                                sprite.PlayAnimation(RStance);
                                nextStep = Next_Step.left;
                            }
                            else if (sprite.Animation == RLRun_RStance)
                            {
                                sprite.PlayAnimation(RStance);
                                nextStep = Next_Step.right;
                            }
                            else if (sprite.Animation == LRRun_LStance)
                            {
                                sprite.PlayAnimation(LStance);
                                nextStep = Next_Step.left;
                            }
                            else if (sprite.Animation == LLRun_LStance)
                            {
                                sprite.PlayAnimation(LStance);
                                nextStep = Next_Step.right;
                            }
                        }
                    }
                    else if (sprite.Animation == RStance_LStance || sprite.Animation == LStance_RStance)
                    {
                        if (sprite.FrameIndex == 2)
                        {
                            sprite.PlayAnimation(AniSwitch(LStance, RStance));
                        }
                    }
                    else
                    {
                        sprite.PlayAnimation(AniSwitch(RStance_RStand, LStance_LStand));
                    }
                    break;
                case Action_State.run:
                    if (sprite.Animation == RRRun_RLRun || sprite.Animation == RLRun_RRRun ||
                        sprite.Animation == LRRun_LLRun || sprite.Animation == LLRun_LRRun)
                    {
                        if (sprite.FrameIndex == 3)
                        {
                            if (keyPress.key_Right >= 1)
                            {
                                if (keyPress.key_Z >= 1)
                                {
                                    if (sprite.Animation == RRRun_RLRun)
                                    {
                                        sprite.PlayAnimation(RLRun_RRRun);
                                        nextStep = Next_Step.right;
                                    }
                                    else if (sprite.Animation == RLRun_RRRun)
                                    {
                                        sprite.PlayAnimation(RRRun_RLRun);
                                        nextStep = Next_Step.left;
                                    }
                                    else
                                    {
                                        sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRRun_RStance, RLRun_RStance), AniSwitchStep(LRRun_LStance, LLRun_LStance)));
                                    }
                                }
                                else
                                {
                                    if (sprite.Animation == RRRun_RLRun)
                                    {
                                        sprite.PlayAnimation(RLRun_RRStep);
                                        nextStep = Next_Step.right;
                                    }
                                    else if (sprite.Animation == RLRun_RRRun)
                                    {
                                        sprite.PlayAnimation(RRRun_RLStep);
                                        nextStep = Next_Step.left;
                                    }
                                    else
                                    {
                                        sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRRun_RStance, RLRun_RStance), AniSwitchStep(LRRun_LStance, LLRun_LStance)));
                                    }
                                }

                            }
                            else if (keyPress.key_Left >= 1)
                            {
                                if (keyPress.key_Z >= 1)
                                {
                                    if (sprite.Animation == LRRun_LLRun)
                                    {
                                        sprite.PlayAnimation(LLRun_LRRun);
                                        nextStep = Next_Step.right;
                                    }
                                    else if (sprite.Animation == LLRun_LRRun)
                                    {
                                        sprite.PlayAnimation(LRRun_LLRun);
                                        nextStep = Next_Step.left;
                                    }
                                    else
                                    {
                                        sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRRun_RStance, RLRun_RStance), AniSwitchStep(LRRun_LStance, LLRun_LStance)));
                                    }
                                }
                                else
                                {
                                    if (sprite.Animation == LRRun_LLRun)
                                    {
                                        sprite.PlayAnimation(LLRun_LRStep);
                                        nextStep = Next_Step.right;
                                    }
                                    else if (sprite.Animation == LLRun_LRRun)
                                    {
                                        sprite.PlayAnimation(LRRun_LLStep);
                                        nextStep = Next_Step.left;
                                    }
                                    else
                                    {
                                        sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRRun_RStance, RLRun_RStance), AniSwitchStep(LRRun_LStance, LLRun_LStance)));
                                    }
                                }
                            }
                            else
                            {
                                sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRRun_RStance, RLRun_RStance), AniSwitchStep(LRRun_LStance, LLRun_LStance)));
                            }
                        }
                    }
                    else if (sprite.Animation == RRRun_RStance || sprite.Animation == RLRun_RStance ||
                        sprite.Animation == LRRun_LStance || sprite.Animation == LLRun_LStance)
                    {
                        if (sprite.FrameIndex == 2)
                        {
                            if (sprite.Animation == RRRun_RStance || sprite.Animation == LRRun_LStance)
                            {
                                nextStep = Next_Step.left;
                                sprite.PlayAnimation(AniSwitch(RStance, LStance));
                                actionState = Action_State.stance;
                            }
                            else if (sprite.Animation == RLRun_RStance || sprite.Animation == LLRun_LStance)
                            {
                                nextStep = Next_Step.right;
                                sprite.PlayAnimation(AniSwitch(RStance, LStance));
                                actionState = Action_State.stance;
                            }
                        }
                    }
                    else if (sprite.Animation == RRRun_RLStep || sprite.Animation == RLRun_RRStep ||
                        sprite.Animation == LRRun_LLStep || sprite.Animation == LLRun_LRStep)
                    {
                        if (sprite.FrameIndex == 3)
                        {
                            sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRStep_RLStep, RLStep_RRStep), AniSwitchStep(LRStep_LLStep, LLStep_LRStep)));
                            actionState = Action_State.walk;
                        }
                    }
                    else
                    {
                        if (sprite.FrameIndex == 3)
                        {
                            sprite.PlayAnimation(AniSwitch(AniSwitchStep(RRRun_RStance, RLRun_RStance), AniSwitchStep(LRRun_LStance, LLRun_LStance)));
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// A switch, based on the direction which the player is facing to decide which animation
        /// </summary>
        private Tools.Animation AniSwitch(Tools.Animation rightSprite, Tools.Animation leftSprite)
        {
            if (facingDirection == Facing_Direction.right)
            {
                return rightSprite;
            }
            else//if (facingDirection == Facing_Direction.left)
            {
                return leftSprite;
            }
        }

        /// <summary>
        /// A switch, based on the step which the player previously took to decide which animation
        /// </summary>
        private Tools.Animation AniSwitchStep(Tools.Animation rightSprite, Tools.Animation leftSprite)
        {
            if (nextStep == Next_Step.right)
            {
                return rightSprite;
            }
            else//if (nextStep == Next_Step.left)
            {
                return leftSprite;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            scarf.Draw(gameTime, spriteBatch);
            sprite.Draw(gameTime, spriteBatch, position, SpriteEffects.None, 0);
        }
    }
    
}
