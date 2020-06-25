using System;
using System.Collections.Generic;
using System.Text;

namespace RocketLibrary
{
    public class Position
    {
        public int X;
        public int Y;
        

        public Position()
        {
            X = 0;
            Y = 0;
        }

        public Position(int x, int y, LandingArea area)
        {
            if (x < 0 || x > area.GetAreaWidth() || y < 0 || y > area.GetAreaHeight()) 
            {
                throw new ArgumentException();
            } 

            X = x;
            Y = y;
        }

        public bool IsPositionInPlatform(LandingPlatform platform)
        {
            return (X >= platform.StartingPosition.X &&
            X <= platform.StartingPosition.X + platform.Width &&
            Y >= platform.StartingPosition.Y &&
            Y <= platform.StartingPosition.Y + platform.Height);
        }


    }
}
