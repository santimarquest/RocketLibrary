namespace RocketLibrary
{
    // A method belongs to an object, if it modifies the state of the object. 
    // If the method does not modify a specific object, it can most likely be static.

    // Properties expose fields.Fields should(almost always) be kept private to a class and accessed via get and set properties.
    // Properties provide a level of abstraction allowing you to change the fields while not affecting the external way they are accessed by the things that use your class.
    public struct Position
    {
        // Must be a public property with a getter and a setter
        public int X { get;}
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Can be static
        public bool IsPositionInPlatform(LandingPlatform platform)
        {
            return (X >= platform.StartingPosition.X &&
            X < platform.StartingPosition.X + platform.Width &&
            Y >= platform.StartingPosition.Y &&
            Y < platform.StartingPosition.Y + platform.Height);
        }

        // Can be static
        public bool IsvalidStartingPosition()
        {
            return (X >= 0 && Y >= 0);
        }
    }
}
