using RocketLibrary;
using System;
using Xunit;

namespace RocketLibraryTest
{
    public class RocketTest
    {
        [Theory]
        [InlineData(5, 5, LandingResult.OK)]
        [InlineData(16,15, LandingResult.OUT_OF_PLATFORM)]
        
        public void CanLandTestWithoutLandedRocket(int x, int y, LandingResult result)
        {
            // Arrange

            var area = LandingArea.CreateArea(100, 100);
            var platform = LandingPlatform.CreatePlatform(10, 10, 5, 5);

            var rocket = new Rocket();
            var position = new Position(x, y, area);


            // Act

            var canland = rocket.CanLand(platform, position);

            // Assert

            Assert.Equal(canland, result);

        }

        [Theory]
        [InlineData(7, 7, LandingResult.CLASH)]
        [InlineData(6, 6, LandingResult.CLASH)]
        [InlineData(9, 9, LandingResult.OK)]
        public void CanLandTestWithLandedRocket(int x, int y, LandingResult result)
        {
            // Arrange

            var area = LandingArea.CreateArea(100, 100);
            var platform = LandingPlatform.CreatePlatform(10, 10, 5, 5);

            var rocket = new Rocket();
            var position = new Position(x, y, area);

            platform.SetPreviousRocketAtPosition(7, 7);


            // Act

            var canland = rocket.CanLand(platform, position);

            // Assert

            Assert.Equal(canland, result);

        }
    }
}
