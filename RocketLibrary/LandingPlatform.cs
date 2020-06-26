using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace RocketLibrary
{
    public  class LandingPlatform
    {
       private static IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory()) // Directory where the json files are located
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

        public int Width { get; set; }
        public int Height { get; set; }

        public Position StartingPosition { get; set; }

        public ConcurrentDictionary<(int, int), LandingResult> LandingResults { get; set; }

        public LandingPlatform(int width, int height, Position startingPosition, ConcurrentDictionary<(int, int), LandingResult> landingResults)
        {
            Width = width;
            Height = height;
            StartingPosition = startingPosition;
            LandingResults = landingResults;
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
