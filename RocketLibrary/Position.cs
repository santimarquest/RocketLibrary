using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

        public static bool IsvalidStartingPosition(Position position)
        {
            return (position.X >= 0 && position.Y>= 0);
        }
    }
}
