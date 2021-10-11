using RoverControl.Validation.Interfaces;
using System;

namespace RoverControl.Validation.Methods
{
    public class PlateauValidation : IValidation
    {
        public void Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(input, "Plateau can not be null");

            var plateau = input.Split(' ');

            if (plateau.Length != 2)
                throw new ArgumentException("Plateau must be two parameters");

            if (!int.TryParse(plateau[0], out int x))
                throw new ArgumentException("Plateau's first parameter is not an integer");

            if (x <= 0)
                throw new ArgumentException("Plateau's first parameter must be greater than 0");

            if (x > 20)
                throw new ArgumentException("Plateau's first parameter must not be greater than 20");

            if (!int.TryParse(plateau[1], out int y))
                throw new ArgumentException("Plateau's second parameter is not an integer");

            if (y <= 0)
                throw new ArgumentException("Plateau's second parameter must be greater than 0");

            if (y > 20)
                throw new ArgumentException("Plateau's second parameter must not be greater than 20");
        }
    }
}
