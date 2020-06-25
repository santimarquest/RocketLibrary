using System;
using System.Collections.Generic;
using System.Text;

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
            return platform.Platform[position.X][position.Y];         
        }
    }
}
