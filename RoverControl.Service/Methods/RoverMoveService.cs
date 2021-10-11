using RoverControl.Model;
using RoverControl.Model.Enums;
using RoverControl.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace RoverControl.Service.Methods
{
    public class RoverMoveService : IRoverMoveService
    {
        public Rover Move(Rover rover, List<Instruction> instructions, Coordinate plateau)
        {
            foreach (var instruction in instructions)
            {
                switch (instruction)
                {
                    case Instruction.L:
                        rover.Direction = GetLeftDirection(rover.Direction);
                        break;
                    case Instruction.R:
                        rover.Direction = GetRightDirection(rover.Direction);
                        break;
                    case Instruction.M:
                        rover.Coordinate = GetCoordinateAfterMovement(rover.Direction, rover.Coordinate, plateau);
                        break;
                }
            }

            return rover;
        }

        private Directions GetLeftDirection(Directions direction)
        {

            switch (direction)
            {
                case Directions.N:
                    return Directions.W;
                case Directions.E:
                    return Directions.N;
                case Directions.S:
                    return Directions.E;
                case Directions.W:
                    return Directions.S;
            }

            return direction;
        }

        private Directions GetRightDirection(Directions direction)
        {
            switch (direction)
            {
                case Directions.N:
                    return Directions.E;
                case Directions.E:
                    return Directions.S;
                case Directions.S:
                    return Directions.W;
                case Directions.W:
                    return Directions.N;
            }

            return direction;
        }

        private Coordinate GetCoordinateAfterMovement(Directions currentDirection, Coordinate currentCoordinate, Coordinate plateau)
        {
            switch (currentDirection)
            {
                case Directions.N:
                    if (currentCoordinate.Y >= plateau.Y)
                        throw new InvalidOperationException("The location you want to go is not correct");

                    currentCoordinate.Y += 1;
                    break;

                case Directions.E:
                    if (currentCoordinate.X >= plateau.X)
                        throw new InvalidOperationException("The location you want to go is not correct");

                    currentCoordinate.X += 1;
                    break;
                case Directions.S:
                    if (currentCoordinate.Y == 0)
                        throw new InvalidOperationException("The location you want to go is not correct");

                    currentCoordinate.Y -= 1;
                    break;

                case Directions.W:
                    if (currentCoordinate.X == 0)
                        throw new InvalidOperationException("The location you want to go is not correct");

                    currentCoordinate.X -= 1;
                    break;
            }

            return currentCoordinate;
        }
    }
}
