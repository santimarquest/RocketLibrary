using RocketLibrary;
using Xunit;

namespace RocketLibraryTest
{
    public class RocketTest
    {
        [Theory]
        [InlineData(5, 5, "ok for landing")]
        [InlineData(16,15, "out of platform")]
        
        public void CanLandTestWithoutLandedRocket(int x, int y, string result)
        {
            // Arrange

            var area = LandingArea.CreateArea(100, 100);
            var startingPosition = new Position(5, 5);
            var platform = LandingPlatform.CreatePlatform(startingPosition, area, 10, 10);

            var rocket = new Rocket();
            var position = new Position(x, y);

            // Act

            var canland = rocket.CanLand(platform, position);

            // Assert

            Assert.Equal(canland, result);
        }

        [Theory]
        [InlineData(7, 7, "clash")]
        [InlineData(6, 6, "clash")]
        [InlineData(7, 8, "clash")]
        [InlineData(6, 7, "clash")]
        [InlineData(9, 9, "ok for landing")]
        public void CanLandTestWithLandedRocket(int x, int y, string result)
        {
            // Arrange

            var area = LandingArea.CreateArea(100, 100);
            var startingPosition = new Position(5, 5);

            // Gettings dimensions from appsettings.json in \RocketLibraryTest\bin\Debug\netcoreapp3.1\appsettings.json
            var platform = LandingPlatform.CreatePlatform(startingPosition, area);

            var rocket = new Rocket();
            var position = new Position(x, y);

            platform.SetPreviousRocketAtPosition(7, 7);

            // Act

            var canland = rocket.CanLand(platform, position);

            // Assert

            Assert.Equal(canland, result);
        }
    }
}
