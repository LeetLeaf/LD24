using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LD24_GermSuperSpy
{
    public class Utilities
    {
        public static Direction FlipDirection(Direction d)
        {
            switch (d)
            {
                case Direction.East:
                    return Direction.West;
                case Direction.North:
                    return Direction.South;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                default:
                    throw new ArgumentException("Direction not supported");
            }
        }
        public static Direction DirectionTo(Player item1, Rectangle item2)
        {
            Point middlePoint1 = new Point((int)item1.Position.X + item1.FrameSize.X / 2, (int)item1.Position.Y + item1.FrameSize.X / 2);
            Point middlePoint2 = new Point((int)item2.X + item2.X / 2, (int)item2.Y + item2.X / 2);

            if (item1.Position.X < middlePoint2.X &&
                middlePoint2.X < item1.Position.X + item1.FrameSize.X)
            {
                if (middlePoint1.Y < middlePoint2.Y)
                {
                    return Direction.South;
                }
                if (middlePoint1.Y > middlePoint2.Y)
                {
                    return Direction.North;
                }
            }
            if (middlePoint1.X < middlePoint2.X)
            {
                return Direction.East;
            }
            if (middlePoint1.X > middlePoint2.X)
            {
                return Direction.West;
            }


            return Direction.East;
        }
        public static Direction DirectionTo(Germ item1, Rectangle item2)
        {
            Point middlePoint1 = new Point((int)item1.Position.X + item1.FrameSize.X / 2, (int)item1.Position.Y + item1.FrameSize.X / 2);
            Point middlePoint2 = new Point((int)item2.X + item2.X / 2, (int)item2.Y + item2.X / 2);

            if (item1.Position.X < middlePoint2.X &&
                middlePoint2.X < item1.Position.X + item1.FrameSize.X)
            {
                if (middlePoint1.Y < middlePoint2.Y)
                {
                    return Direction.South;
                }
                if (middlePoint1.Y > middlePoint2.Y)
                {
                    return Direction.North;
                }
            }
            if (middlePoint1.X < middlePoint2.X)
            {
                return Direction.East;
            }
            if (middlePoint1.X > middlePoint2.X)
            {
                return Direction.West;
            }


            return Direction.East;
        }

        public static Direction NumToDirection(int num)
        {
            if (num == 3)
                return Direction.East;
            if (num == 2)
                return Direction.North;
            if (num == 1)
                return Direction.West;
            if (num == 0)
                return Direction.South;

            return Direction.North;
        }
    }
}
