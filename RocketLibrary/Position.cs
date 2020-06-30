namespace RocketLibrary
{
    public class Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsPositionInPlatform(LandingPlatform platform)
        {
            return (X >= platform.StartingPosition.X &&
            X < platform.StartingPosition.X + platform.Width &&
            Y >= platform.StartingPosition.Y &&
            Y < platform.StartingPosition.Y + platform.Height);
        }

        public bool IsvalidStartingPosition()
        {
            return (X >= 0 && Y >= 0);
        }
    }
}
