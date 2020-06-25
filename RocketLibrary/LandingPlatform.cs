using System;
using System.Collections.Generic;
using System.Text;

namespace RocketLibrary
{
   public  class LandingPlatform
    {
        public int Width;
        public int Height;

        public Position StartingPosition { get; set; }

        public LandingResult[][] Platform;

        private LandingPlatform(int width, int height)
        {
            Width = width;
            Height = height;
            StartingPosition = new Position();
        }

        public static LandingPlatform CreatePlatform(int widthPlatform, int heightPlatform, Position startingPosition, LandingArea area)
        {
            var landingPlatform = new LandingPlatform(widthPlatform, heightPlatform);
            landingPlatform.StartingPosition = startingPosition;

            if (!IsValidPlatform(landingPlatform, area))
            {
                throw new ArgumentException();
            }

            var widthArea = area.GetAreaWidth();
            var heightArea = area.GetAreaHeight();

            landingPlatform.Platform = new LandingResult[widthArea][];
            for (int i = 0; i < widthArea; i++)
            {
                landingPlatform.Platform[i] = new LandingResult[heightArea];
                for (int j = 0; j < heightArea; j++)
                    landingPlatform.Platform[i][j] = LandingResult.OUT_OF_PLATFORM;
            }


             for (int i = startingPosition.X; i < startingPosition.X + widthPlatform; i++)
            {
                for (int j = startingPosition.Y; j < startingPosition.Y + heightPlatform; j++)
                {
                    landingPlatform.Platform[i][j] = LandingResult.OK;
                }
            }

            return landingPlatform;
        }

        private static bool IsValidPlatform(LandingPlatform landingPlatform, LandingArea area)
        {
            return landingPlatform.StartingPosition.X >= 0 &&
                landingPlatform.StartingPosition.X + landingPlatform.Width <= area.GetAreaWidth() &&
                landingPlatform.StartingPosition.Y >= 0 &&
                landingPlatform.StartingPosition.Y + landingPlatform.Height <= area.GetAreaHeight();
        }

        public void SetPreviousRocketAtPosition(int x, int y)
        {
            Platform[x][y] = LandingResult.CLASH;

            Platform[x-1][y] = LandingResult.CLASH;
            Platform[x + 1][y] = LandingResult.CLASH;

            Platform[x - 1][y -1] = LandingResult.CLASH;
            Platform[x - 1][y] = LandingResult.CLASH;
            Platform[x - 1][y + 1] = LandingResult.CLASH;

            Platform[x + 1][y - 1] = LandingResult.CLASH;
            Platform[x + 1][y] = LandingResult.CLASH;
            Platform[x + 1][y + 1] = LandingResult.CLASH;

        }

    }
}
