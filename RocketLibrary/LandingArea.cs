using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
