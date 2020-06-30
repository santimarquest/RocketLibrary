using System;
using System.Collections.Concurrent;

namespace RocketLibrary
{
    public class LandingPlatformBuilder
    {
        public int WidthPlatform;
        public int HeightPlatform;

        public Position StartingPosition { get; set; }

        public ConcurrentDictionary<(int, int), LandingResult> LandingResults;
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
            if (!startingPosition.IsvalidStartingPosition())
            {
                throw new ArgumentException();
            }
            StartingPosition = startingPosition;
            return this;
        }

        public LandingPlatformBuilder IntoArea (LandingArea landingArea)
        {
            if (!IsStartingPositionInArea(landingArea))
            {
                throw new ArgumentException();
            }

            if (!LandingPlatform.IsValidPlatform(WidthPlatform, HeightPlatform, StartingPosition, landingArea))
            {
                throw new ArgumentException();
            }

            LandingResults = new ConcurrentDictionary<(int, int), LandingResult>();

            return this;
        }

        private bool IsStartingPositionInArea(LandingArea landingArea)
        {
            return (StartingPosition.X <= landingArea.GetAreaWidth() &&
                       StartingPosition.Y <= landingArea.GetAreaHeight());
        }

        public LandingPlatformBuilder WithDefaultLandingResult()
        {
            for (int i = StartingPosition.X; i < StartingPosition.X + WidthPlatform; i++)
            {
                for (int j = StartingPosition.Y; j < StartingPosition.Y + HeightPlatform; j++)
                {
                    LandingResults[(i,j)] = LandingResult.OK;
                }
            }

            return this;
        }

        public LandingPlatform Build()
        {
            return LandingPlatform.LandingPlatformInstance(WidthPlatform, HeightPlatform, StartingPosition, LandingResults);
        }

    }
}
