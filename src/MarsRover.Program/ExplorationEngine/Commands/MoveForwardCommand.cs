using MarsRover.Program.Common.Enums;
using MarsRover.Program.Models;

namespace MarsRover.Program.ExplorationEngine.Commands
{
    public class MoveForwardCommand : IExplorationCommand
    {
        public void Explore(RoverLocation roverLocation)
        {
            switch (roverLocation.Direction)
            {
                case Direction.North:
                    roverLocation.YPosition += 1;
                    break;
                case Direction.East:
                    roverLocation.XPosition += 1;
                    break;
                case Direction.South:
                    roverLocation.YPosition -= 1;
                    break;
                case Direction.West:
                    roverLocation.XPosition -= 1;
                    break;
            }
        }
    }
}