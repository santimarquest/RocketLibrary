using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.CompilerServices;

namespace RocketLibrary
{
    public sealed class LandingPlatform
    {
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

        public ConcurrentDictionary<(int, int), LandingResult> LandingResults { get; set; }

        private LandingPlatform()
        {

        }
        public LandingPlatform(int width, int height, Position startingPosition, ConcurrentDictionary<(int, int), LandingResult> landingResults)
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
                .WithDefaultLandingResult()
                .Build();
        }

        internal static LandingPlatform LandingPlatformInstance(int widthPlatform, int heightPlatform, Position startingPosition, ConcurrentDictionary<(int, int), LandingResult> landingResults)
        {
            new LandingPlatform(widthPlatform, heightPlatform, startingPosition, landingResults);
            return Instance;
        }

        public static bool IsValidPlatform(int width, int height, Position startingPosition, LandingArea landingArea)
        {
            return startingPosition.X >= 0 &&
                startingPosition.X + width <= landingArea.GetAreaWidth() &&
                startingPosition.Y >= 0 &&
                startingPosition.Y + height <= landingArea.GetAreaHeight();
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
            if (position.IsPositionInPlatform(this) && LandingResults[(position.X, position.Y)] == LandingResult.OK)
            {
                LandingResults[(position.X,position.Y)] = LandingResult.CLASH;
            }
        }
    }
}
