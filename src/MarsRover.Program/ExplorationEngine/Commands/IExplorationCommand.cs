using MarsRover.Program.Models;

namespace MarsRover.Program.ExplorationEngine.Commands
{
    public interface IExplorationCommand
    {
        void Explore(RoverLocation roverLocation);
    }
}