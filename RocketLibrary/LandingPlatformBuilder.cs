using System;
using System.Collections.Concurrent;

namespace RocketLibrary
{
    // Properties expose fields.Fields should(almost always) be kept private to a class and accessed via get and set properties.
    // Properties provide a level of abstraction allowing you to change the fields while not affecting the external way they are accessed by the things that use your class.
    public class LandingPlatformBuilder
    {
        // All these fields can be private
        private int WidthPlatform;
        private int HeightPlatform;

        private Position StartingPosition;

        private ConcurrentDictionary<Position, LandingResult> LandingResults { get; set; }

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

            LandingResults = new ConcurrentDictionary<Position, LandingResult>();

            return this;
        }

        private bool IsStartingPositionInArea(LandingArea landingArea)
        {
            return (StartingPosition.X <= landingArea.Width &&
                       StartingPosition.Y <= landingArea.Height);
        }

        public LandingPlatform Build()
        {
            return LandingPlatform.LandingPlatformInstance(WidthPlatform, HeightPlatform, StartingPosition, LandingResults);
        }

    }
}
