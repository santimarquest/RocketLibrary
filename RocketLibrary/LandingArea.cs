using System;

namespace RocketLibrary
{
    public class LandingArea
    {
        private readonly int Width;
        private readonly int Height;

        private LandingArea (int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static LandingArea CreateArea(int x, int y)
        {
            // Factory method with guard clause
            
            if (x < 0 || y < 0)
            {
                throw new ArgumentException();
            }

            return new LandingArea(x, y);
        }

        public int GetAreaWidth ()
        {
            return Width;
        }

        public int GetAreaHeight()
        {
            return Height;
        }
    }
}
