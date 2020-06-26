using System;
using System.Collections.Generic;
using System.Text;

namespace RocketLibrary
{
    public class LandingPlatformBuilder
    {
        public int WidthPlatform;
        public int HeightPlatform;

        public Position StartingPosition { get; set; }

        public LandingResult[][] LandingResults;
        public static LandingPlatformBuilder CreateLandingPlatform()
        {
            return new LandingPlatformBuilder();
        }

        public LandingPlatformBuilder WithWidthAndHeight (int width, int height)
        {
            if (width < 0  || height < 0) {
                throw new ArgumentException();
            }
            
            WidthPlatform = width;
            HeightPlatform = height;

            return this;
        }

        public LandingPlatformBuilder WithStartingPosition (Position startingPosition)
        {
            if (!Position.IsvalidStartingPosition(startingPosition))
            {
                throw new ArgumentException();
            }
            StartingPosition = startingPosition;
            return this;
        }

        public LandingPlatformBuilder IntoArea (LandingArea landingArea)
        {
            if (!LandingPlatform.IsValidPlatform(WidthPlatform, HeightPlatform, StartingPosition, landingArea))
            {
                throw new ArgumentException();
            }

            var widthArea = landingArea.GetAreaWidth();
            var heightArea = landingArea.GetAreaHeight();

            LandingResults = new LandingResult[widthArea][];
            for (int i = 0; i < widthArea; i++)
            {
                LandingResults[i] = new LandingResult[heightArea];
                for (int j = 0; j < heightArea; j++)
                    LandingResults[i][j] = LandingResult.OUT_OF_PLATFORM;
            }

            return this;

        }

        public LandingPlatformBuilder WithDefaultLandingResult()
        {
            for (int i = StartingPosition.X; i < StartingPosition.X + WidthPlatform; i++)
            {
                for (int j = StartingPosition.Y; j < StartingPosition.Y + HeightPlatform; j++)
                {
                    LandingResults[i][j] = LandingResult.OK;
                }
            }

            return this;
        }

        public LandingPlatform Build()
        {
            return new LandingPlatform(WidthPlatform, HeightPlatform, StartingPosition, LandingResults);
        }

    }
}
