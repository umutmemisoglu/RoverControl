using RoverControl.Model.Enums;

namespace RoverControl.Model
{
    public class Rover
    {
        public Rover(Coordinate startingCoordinate, Directions startingDirection)
        {
            Coordinate = startingCoordinate;
            Direction = startingDirection;
        }

        public Coordinate Coordinate { get; set; }

        public Directions Direction { get; set; }

        public string GetLocation() => string.Join(' ', Coordinate.X, Coordinate.Y, Direction);
    }
}
