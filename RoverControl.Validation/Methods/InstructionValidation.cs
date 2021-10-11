using RoverControl.Validation.Interfaces;
using System;
using System.Linq;

namespace RoverControl.Validation.Methods
{
    public class InstructionValidation : IValidation
    {
        public void Validate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(input, "Instruction can not be null");

            if (input.Length > 50)
                throw new ArgumentException("Instruction must not be greater than 50");

            var diff = input.Where(x => !new[] { 'L', 'R', 'M' }.Contains(x));
            if (diff.Any())
                throw new ArgumentException("Instruction must be a few of these L, R or M");

        }
    }
}
