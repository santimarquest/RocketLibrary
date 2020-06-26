using System.Collections.Generic;

namespace RocketLibrary
{
    public class Rocket
    {
        private Dictionary<LandingResult, string> ResultValue = new Dictionary<LandingResult, string>
        {
            { LandingResult.OK, "ok for landing" },
            { LandingResult.OUT_OF_PLATFORM, "out of platform" },
            { LandingResult.CLASH, "clash" }
        };
    public string CanLand (LandingPlatform platform, Position position)
        {
            if (!position.IsPositionInPlatform(platform))
            {
                return ResultValue[LandingResult.OUT_OF_PLATFORM];
            }           
            return ResultValue[platform.LandingResults[position.X][position.Y]];         
        }
    }
}
