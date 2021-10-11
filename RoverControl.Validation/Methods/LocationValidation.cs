using RoverControl.Validation.Interfaces;
using System;
using System.Linq;

namespace RoverControl.Validation.Methods
{
    public class LocationValidation : IValidation
    {
        public void Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(input, "Location can not be null");

            var location = input.Split(' ');

            if (location.Length != 3)
                throw new ArgumentException("Location must be three parameters");

            if (!int.TryParse(location[0], out int x))
                throw new ArgumentException("Location's first parameter is not an integer");

            if (x <= 0)
                throw new ArgumentException("Location's first parameter must be greater than 0");

            if (x > 20)
                throw new ArgumentException("Location's first parameter must not be greater than 20");

            if (!int.TryParse(location[1], out int y))
                throw new ArgumentException("Location's second parameter is not an integer");

            if (y <= 0)
                throw new ArgumentException("Location's second parameter must be greater than 0");

            if (y > 20)
                throw new ArgumentException("Location's second parameter must not be greater than 20");

            if (location[2].Length != 1)
                throw new ArgumentException("Location's third parameter must be one character");

            if (!new[] { "N", "W", "E", "S" }.Any(location[2].Contains))
                throw new ArgumentException("Location's third parameter must be one of these N, W, E or S");
        }
    }
}
