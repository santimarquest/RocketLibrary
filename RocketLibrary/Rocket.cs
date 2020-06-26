namespace RocketLibrary
{
    public class Rocket
    {
        public LandingResult CanLand (LandingPlatform platform, Position position)
        {
            if (!position.IsPositionInPlatform(platform))
            {
                return LandingResult.OUT_OF_PLATFORM;
            }           
            return platform.LandingResults[position.X][position.Y];         
        }
    }
}
