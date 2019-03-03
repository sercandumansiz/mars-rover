using MarsRover.Program.Common.Enums;
using MarsRover.Program.ExplorationEngine.Commands;
using MarsRover.Program.Models;

namespace MarsRover.Program.ExplorationEngine.Factory
{
    public interface IExplorationFactory
    {
        IExplorationCommand ExecuteInstruction(Instruction instruction);
    }
}