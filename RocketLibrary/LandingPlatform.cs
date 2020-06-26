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

        public LandingResult[][] LandingResults;

        public LandingPlatform(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public LandingPlatform(int width, int height, Position startingPosition, LandingResult[][] landingResults) : this(width, height)
        {
            StartingPosition = startingPosition;
            LandingResults = landingResults;
        }

        public static LandingPlatform CreatePlatform(int widthPlatform, int heightPlatform, Position startingPosition, LandingArea landingArea)
        {
            return LandingPlatformBuilder.CreateLandingPlatform()
                .WithWidthAndHeight(widthPlatform, heightPlatform)
                .WithStartingPosition(startingPosition)
                .IntoArea(landingArea)
                .WithDefaultLandingResult()
                .Build();
        }

        internal static bool IsValidPlatform(int width, int height, Position startingPosition, LandingArea landingArea)
        {
            return startingPosition.X >= 0 &&
                startingPosition.X + width <= landingArea.GetAreaWidth() &&
                startingPosition.Y >= 0 &&
                startingPosition.Y + height <= landingArea.GetAreaHeight();
        }

        public void SetPreviousRocketAtPosition(int x, int y)
        {
            LandingResults[x][y] = LandingResult.CLASH;

            LandingResults[x-1][y] = LandingResult.CLASH;
            LandingResults[x + 1][y] = LandingResult.CLASH;

            LandingResults[x - 1][y -1] = LandingResult.CLASH;
            LandingResults[x - 1][y] = LandingResult.CLASH;
            LandingResults[x - 1][y + 1] = LandingResult.CLASH;

            LandingResults[x + 1][y - 1] = LandingResult.CLASH;
            LandingResults[x + 1][y] = LandingResult.CLASH;
            LandingResults[x + 1][y + 1] = LandingResult.CLASH;

        }

    }
}
