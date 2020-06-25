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

        public static LandingPlatform CreatePlatform(int width, int height, int startingX, int startingY)
        {
            var landingPlatform = new LandingPlatform(width, height);
            landingPlatform.StartingPosition.X = startingX;
            landingPlatform.StartingPosition.Y = startingY;
            
            landingPlatform.Platform = new LandingResult[100][];
            for (int i = 0; i < 100; i++)
            {
                landingPlatform.Platform[i] = new LandingResult[100];
                for (int j = 0; j < 100; j++)
                    landingPlatform.Platform[i][j] = LandingResult.OUT_OF_PLATFORM;
            }


                for (int i = startingX; i < startingX + width; i++)
            {
                for (int j = startingY; j < startingY + height; j++)
                {
                    landingPlatform.Platform[i][j] = LandingResult.OK;
                }
            }

            return landingPlatform;
        }

        //private void SetLandingResultAtPosition(int x, int y, LandingResult landingResult)
        //{
        //    Platform[x][y] = landingResult;
        //}

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
