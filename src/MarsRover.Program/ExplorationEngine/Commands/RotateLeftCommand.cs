using MarsRover.Program.Common.Enums;
using MarsRover.Program.Models;

namespace MarsRover.Program.ExplorationEngine.Commands
{
    public class RotateLeftCommand : IExplorationCommand
    {
        public void Explore(RoverLocation roverLocation)
        {
            switch (roverLocation.Direction)
            {
                case Direction.North:
                    roverLocation.Direction = Direction.West;
                    break;
                case Direction.East:
                    roverLocation.Direction = Direction.North;
                    break;
                case Direction.South:
                    roverLocation.Direction = Direction.East;
                    break;
                case Direction.West:
                    roverLocation.Direction = Direction.South;
                    break;
            }
        }
    }
}