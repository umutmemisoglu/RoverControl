using RoverControl.Model;
using RoverControl.Model.Enums;
using System.Collections.Generic;

namespace RoverControl.Service.Interfaces
{
    public interface IRoverMoveService
    {
        Rover Move(Rover rover, List<Instruction> instructions, Coordinate plateau);
    }
}
