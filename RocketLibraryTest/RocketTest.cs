using RocketLibrary;
using System;
using Xunit;

namespace RocketLibraryTest
{
    public class RocketTest
    {
        [Theory]
        [InlineData(5, 5, "ok for landing")]
        [InlineData(5, 14, "ok for landing")]
        [InlineData(14, 14, "ok for landing")]
        [InlineData(14, 5, "ok for landing")]
        [InlineData(15, 15, "out of platform")]
        [InlineData(16,15, "out of platform")]
        
        public void CanLandTestWithoutLandedRocket(int x, int y, string result)
        {
            // Arrange

            var area = LandingArea.CreateArea(100, 100);
            var startingPosition = new Position(5, 5);
            var platform = LandingPlatform.CreatePlatform(startingPosition, area, 10, 10);

            var position = new Position(x, y);

            // Act

            var canland = platform.CanLand(platform, position);

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

            var platform = LandingPlatform.CreatePlatform(startingPosition, area, 10, 10);

            var position = new Position(x, y);

            platform.SetPreviousRocketAtPosition(7, 7);

            // Act

            var canland = platform.CanLand(platform, position);

            // Assert

            Assert.Equal(canland, result);
        }

       [Fact]
        public void PlatformDimensionConfigurationTest()
        {
            // Arrange

            var area = LandingArea.CreateArea(100, 100);
            var startingPosition = new Position(5, 5);

            // Act

            // Getting platform dimensions from \RocketLibraryTest\bin\Debug\netcoreapp3.1\appsettings.json
            var platform = LandingPlatform.CreatePlatform(startingPosition, area);

            // Assert

            Assert.Equal(3, platform.Width);
            Assert.Equal(3, platform.Height);
        }

        [Fact]
        public void PlatformDimensionConfigurationExceptionTest()
        {
            // Arrange

            var area = LandingArea.CreateArea(100, 100);
            var startingPosition = new Position(5, 5);

            Assert.Throws<ArgumentException>(() => LandingPlatform.CreatePlatform(startingPosition, area, 1000, 1000));
        }
    }
}
