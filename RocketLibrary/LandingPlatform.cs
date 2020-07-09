using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace RocketLibrary
{
    // A method belongs to an object, if it modifies the state of the object. 
    // If the method does not modify a specific object, it can most likely be static.
    public sealed class LandingPlatform
    {

        private Dictionary<LandingResult, string> ResultValue = new Dictionary<LandingResult, string>
        {
            // Magic strings are not correct -> better work with constants or configurable messages
            { LandingResult.OK, "ok for landing" },
            { LandingResult.OUT_OF_PLATFORM, "out of platform" },
            { LandingResult.CLASH, "clash" }
        };

        private static IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory()) // Directory where the json files are located
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

        private static readonly Lazy<LandingPlatform> singletonLandingPlatform =
              new Lazy<LandingPlatform>(() => new LandingPlatform());

        public static LandingPlatform Instance
       => singletonLandingPlatform.Value;

        public int Width { get; set; }
        public int Height { get; set; }

        public Position StartingPosition { get; set; }

        public ConcurrentDictionary<Position, LandingResult> LandingResults { get; set; }

        private LandingPlatform()
        {

        }
        public LandingPlatform(int width, int height, Position startingPosition, ConcurrentDictionary<Position, LandingResult> landingResults)
        {
            Instance.Width = width;
            Instance.Height = height;
            Instance.StartingPosition = startingPosition;
            Instance.LandingResults = landingResults;
        }

        public static LandingPlatform CreatePlatform(Position startingPosition, LandingArea landingArea, int? widthPlatform = 0, int? heightPlatform = 0)
        {
            int.TryParse(configuration["WidthPlatform"], out int widthP);
            var width = (widthPlatform > 0) ? widthPlatform : widthP;

            int.TryParse(configuration["HeightPlatform"], out int heightP);
            var height = heightPlatform > 0 ? heightPlatform : heightP;

            return LandingPlatformBuilder.CreateLandingPlatform()
                .WithWidthAndHeight(width.Value, height.Value)
                .WithStartingPosition(startingPosition)
                .IntoArea(landingArea)
                .Build();
        }

        internal static LandingPlatform LandingPlatformInstance(int widthPlatform, int heightPlatform, Position startingPosition, ConcurrentDictionary<Position, LandingResult> landingResults)
        {
            new LandingPlatform(widthPlatform, heightPlatform, startingPosition, landingResults);
            return Instance;
        }

        public static bool IsValidPlatform(int width, int height, Position startingPosition, LandingArea landingArea)
        {
            return startingPosition.X >= 0 &&
                startingPosition.X + width <= landingArea.Width &&
                startingPosition.Y >= 0 &&
                startingPosition.Y + height <= landingArea.Height;
        }

        public void SetPreviousRocketAtPosition(int x, int y)
        {
            for (int i = x-1; i <= x + 1; i++ )
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    SetLandingResultClashForPosition(new Position(i, j));
                }
            }

        }

        private void SetLandingResultClashForPosition(Position position)
        {
            if (position.IsPositionInPlatform(this))
            {
                LandingResults[position] = LandingResult.CLASH;
            }
        }

        public string CanLand(LandingPlatform platform, Position position)
        {
            if (!position.IsPositionInPlatform(platform))
            {
                return ResultValue[LandingResult.OUT_OF_PLATFORM];
            }

            if (platform.LandingResults.ContainsKey(position)) 
            {
                //return ResultValue[platform.LandingResults[position]];
                return ResultValue[LandingResult.CLASH];
            }

            return ResultValue[LandingResult.OK];
        }

    }
}
