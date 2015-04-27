using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD24_GermSuperSpy
{
    public class Germ
    {

        private Dictionary<Direction, Point> frames;

        public string GermName;
        public Texture2D texture;
        public Vector2 Position;
        public Direction characterFace;
        public Point FrameSize { get; set; }
        public Point currentFrame;
        public Tuple<Direction, Vector2> ShadowMovement;

        private Random random = new Random();
        private bool countDown;
        private int tempTime;
        private float speed;


        public Germ(Texture2D texture, string GermName)
        {
            this.texture = texture;
            this.GermName = GermName;
            switch (random.Next(0,3))
            { 
                case 0:
                    characterFace = Direction.South;
                    break;
                case 1:
                    characterFace = Direction.West;
                    break;
                case 2:
                    characterFace = Direction.North;
                    break;
                case 3:
                    characterFace = Direction.East;
                    break;

            }

            frames = new Dictionary<Direction, Point>();
            frames.Add(Direction.North, new Point(0, 0));
            frames.Add(Direction.South, new Point(3, 0));
            frames.Add(Direction.East, new Point(2, 0));
            frames.Add(Direction.West, new Point(1, 0));


            switch (random.Next(0, 3))
            { 
                case 0:
                    Position = new Vector2(random.Next(26, 115), random.Next(28, 107));
                break;
                case 1:
                Position = new Vector2(random.Next(81, 240), random.Next(95, 146));
                break;
                case 2:
                Position = new Vector2(random.Next(0, 240), random.Next(187, 240));
                break;
            }
            

            FrameSize = new Point(16, 16);
            currentFrame = frames[characterFace];
            speed = 0.3f;
        }
        public Germ(Texture2D texture, string GermName, Vector2 position)
        {
            this.texture = texture;
            this.GermName = GermName;
            switch (random.Next(0, 3))
            {
                case 0:
                    characterFace = Direction.South;
                    break;
                case 1:
                    characterFace = Direction.West;
                    break;
                case 2:
                    characterFace = Direction.North;
                    break;
                case 3:
                    characterFace = Direction.East;
                    break;

            }

            frames = new Dictionary<Direction, Point>();
            frames.Add(Direction.North, new Point(0, 0));
            frames.Add(Direction.South, new Point(3, 0));
            frames.Add(Direction.East, new Point(2, 0));
            frames.Add(Direction.West, new Point(1, 0));

            Position = position;

            FrameSize = new Point(16, 16);
            currentFrame = frames[characterFace];

        }

        public Rectangle CollisionRec()
        {
            return new Rectangle((int)Position.X + 5, (int)Position.Y + 2,
                FrameSize.X - 5, FrameSize.Y - 3);
        }

        public Rectangle RenderFrame()
        {
            return new Rectangle((int)FrameSize.X * (int)currentFrame.X, (int)FrameSize.Y * (int)currentFrame.Y,
                (int)FrameSize.X, (int)FrameSize.Y);
        }

        public void moveTo(Direction direction)
        {
           
            if (direction == Direction.West)
                Position.X += 1;
            if (direction == Direction.South)
                Position.Y -= 1;
            if (direction == Direction.North)
                Position.Y += 1;
            if (direction == Direction.East)
                Position.X -= 1;
        }

        public void moveForward(float Speed)
        {
            if (GermName.Equals("Protectors") && Game1.currentMission.Equals(Game1.mission3))
            {
                Speed = 0.5f;

            }
            else
            {
                Speed = 0.3f;
            }

            if (characterFace == Direction.West)
                Position.X += Speed;
            if (characterFace == Direction.South)
                Position.Y -= Speed;
            if (characterFace == Direction.North)
                Position.Y += Speed;
            if (characterFace == Direction.East)
                Position.X -= Speed;
        }

        public void Chase(Player Desination)
        {
            characterFace = Utilities.FlipDirection(Utilities.DirectionTo(this, Desination.CollisionRec()));
        }

        public void Update(GameTime gameTime)
        {
            ShadowMovement = new Tuple<Direction, Vector2>(characterFace, Position);

            if (GermName.Equals("Protectors"))
            {
                if (Game1.player.randomPos)
                {
                    if (countDown)
                    {
                        tempTime -= gameTime.ElapsedGameTime.Milliseconds;
                        if (tempTime < 0)
                            countDown = false;
                    }
                    if (!countDown)
                    {
                        characterFace = Utilities.NumToDirection(random.Next(0, 4));
                        countDown = true;
                        tempTime = random.Next(100, 1000);
                    }
                }
                else
                { 
                    Chase(Game1.player);
                }
                
            }
            else
            {
                if (countDown)
                {
                    tempTime -= gameTime.ElapsedGameTime.Milliseconds;
                    if (tempTime < 0)
                        countDown = false;
                }
                if (!countDown)
                {
                    characterFace = Utilities.NumToDirection(random.Next(0, 4));
                    countDown = true;
                    tempTime = random.Next(100, 1000);
                }
                
            }
            currentFrame = frames[characterFace];
            moveForward(speed);

            if (Position.X < 0)
                Position.X = 0;
            if (Position.Y < 0)
                Position.Y = 0;
            if (Position.X > 256 - FrameSize.X)
                Position.X = 256 - FrameSize.X;
            if (Position.Y > 256 - FrameSize.Y)
                Position.Y = 256 - FrameSize.Y;

            for (int i = 0; i < Game1.map.collisionSpots.Count; i++)
            {
                if (CollisionRec().Intersects(Game1.map.collisionSpots[i]))
                {
                    characterFace = ShadowMovement.Item1;
                    Position = ShadowMovement.Item2;
                }
            }
            foreach(Germ colGerm in Game1.currentMission.Germs.Where(
                    m => !m.Equals(this)))
            {
                if(CollisionRec().Intersects(colGerm.CollisionRec()))
                {
                    Position = ShadowMovement.Item2;
                }
            }
            foreach (Germ colGerm in Game1.currentMission.Protectors.Where(
                    m => !m.Equals(this)))
            {
                if (CollisionRec().Intersects(colGerm.CollisionRec()))
                {
                    Position = ShadowMovement.Item2;
                }
            }
            if(CollisionRec().Intersects(Game1.player.CollisionRec()))
            {
                 Position = ShadowMovement.Item2;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, RenderFrame(), Color.White);
        }
    }
}
