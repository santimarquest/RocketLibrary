namespace RocketLibrary
{
    public  class LandingPlatform
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Position StartingPosition { get; set; }

        public LandingResult[][] LandingResults { get; set; }

        public LandingPlatform(int width, int height, Position startingPosition, LandingResult[][] landingResults)
        {
            Width = width;
            Height = height;
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
            if (position.IsPositionInPlatform(this) && LandingResults[position.X][position.Y] == LandingResult.OK)
            {
                LandingResults[position.X][position.Y] = LandingResult.CLASH;
            }
        }
    }
}
