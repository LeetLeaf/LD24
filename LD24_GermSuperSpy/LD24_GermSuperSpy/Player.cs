using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD24_GermSuperSpy
{
    public class Player
    {
        private Dictionary<Direction, Point> frames;
        Texture2D texture;
        
        public Vector2 Position;
        public Direction characterFace;
        public Point FrameSize { get; set; }
        public Point currentFrame;
        public Tuple<Direction, Vector2> ShadowMovement;

        private bool attack =false;
        private bool jumpCoolDown = false;
        private int timeJump = 600;
        private Random random;
        public  bool randomPos;

        
        public Player()
        {
            texture = Game1.Textures["SpyGuy"];
            frames = new Dictionary<Direction,Point>();
            frames.Add(Direction.North,new Point(3,0));
            frames.Add(Direction.South, new Point(0,0));
            frames.Add(Direction.East, new Point(2,0));
            frames.Add(Direction.West,new Point(1,0));
            Position = new Vector2(10, 27);

            characterFace = Direction.South;

            FrameSize = new Point(16, 16);
            currentFrame = new Point(0, 0);
            random = new Random();
            randomPos = false;
        }

        public Rectangle CollisionRec()
        {
            return new Rectangle((int)Position.X + 5, (int)Position.Y + 2,
                FrameSize.X - 5, FrameSize.Y - 3);
        }

        public Vector2 getPosition()
        {
          
                return Position;
            
        }

        public Rectangle RenderFrame()
        {
            return new Rectangle((int)FrameSize.X * (int)currentFrame.X, (int)FrameSize.Y * (int)currentFrame.Y,
                (int)FrameSize.X, (int)FrameSize.Y);
        }


        public void KnockBack(Direction direction, int length)
        {
            if (direction == Direction.East)
                Position.X -= length;
            if (direction == Direction.West)
                Position.X += length;
            if (direction == Direction.South)
                Position.Y -= length;
            if (direction == Direction.North)
                Position.Y += length;
        }

        public void jump(Direction direction, int length)
        {
            if (direction == Direction.East)
                Position.X -= length;
            if (direction == Direction.West)
                Position.X += length;
            if (direction == Direction.South)
                Position.Y += length;
            if (direction == Direction.North)
                Position.Y -= length;
        }

        public void Update(GameTime gameTime)
        {
                if (Game1.currentMission.Ready)
            {
                ShadowMovement = new Tuple<Direction, Vector2>(characterFace, Position);
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    Position.Y -= 1;
                    characterFace = Direction.North;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    Position.Y += 1;
                    characterFace = Direction.South;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    Position.X -= 1;
                    characterFace = Direction.East;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    Position.X += 1;
                    characterFace = Direction.West;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.C) && !jumpCoolDown &&(Game1.currentMission.Equals(Game1.mission2) ||Game1.currentMission.Equals(Game1.mission3)))
                {
                    jump(characterFace, 30);
                    jumpCoolDown = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Z) && Game1.currentMission.Equals(Game1.mission3))
                {
                    randomPos = true;
                }
                else
                {
                    randomPos = false;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.X))
                {
                    attack = true;
                }
                else
                {
                    attack = false;
                }
                currentFrame = frames[characterFace];

                if (Position.X < 0)
                    Position.X = 0;
                if (Position.Y < 0)
                    Position.Y = 0;
                if (Position.X > 256 - FrameSize.X)
                    Position.X = 256 - FrameSize.X;
                if (Position.Y > 256 - FrameSize.Y)
                    Position.Y = 256 - FrameSize.Y;


                if (jumpCoolDown)
                {
                    if (timeJump > 0)
                        timeJump -= gameTime.ElapsedGameTime.Milliseconds;
                    else
                    {
                        timeJump = 600;
                        jumpCoolDown = false;
                    }
                }

                for (int i = 0; i < Game1.map.collisionSpots.Count; i++)
                {
                    if (CollisionRec().Intersects(Game1.map.collisionSpots[i]))
                    {
                        characterFace = ShadowMovement.Item1;
                        Position = ShadowMovement.Item2;
                    }

                }
                for (int i = 0; i < Game1.currentMission.Germs.Count; i++)
                {
                    if (CollisionRec().Intersects(Game1.currentMission.Germs[i].CollisionRec()) && attack)
                    {
                        if (Game1.currentMission.Germs[i].GermName.Equals("Target"))
                        {
                            Game1.currentMission.Finished = true;
                        }
                        Game1.currentMission.Germs.RemoveAt(i);
                    }
                }
                foreach (Germ colGerm in Game1.currentMission.Protectors)
                {
                    if (CollisionRec().Intersects(colGerm.CollisionRec()))
                    {
                        Position = ShadowMovement.Item2;
                    }
                }
                foreach (Germ colGerm in Game1.currentMission.Germs)
                {
                    if (CollisionRec().Intersects(colGerm.CollisionRec()))
                    {
                        Position = ShadowMovement.Item2;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, RenderFrame(), Color.White);
        }
    }
}
