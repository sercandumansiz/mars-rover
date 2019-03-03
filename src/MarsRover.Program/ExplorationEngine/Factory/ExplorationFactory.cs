using MarsRover.Program.Common.Enums;
using MarsRover.Program.ExplorationEngine.Commands;
using MarsRover.Program.Models;

namespace MarsRover.Program.ExplorationEngine.Factory
{
    public class ExplorationFactory : IExplorationFactory
    {
        public IExplorationCommand ExecuteInstruction(Instruction instruction)
        {
            IExplorationCommand explorationCommand = null;

            switch (instruction)
            {
                case Instruction.RotateRight:
                    explorationCommand = new RotateRightCommand();
                    break;
                case Instruction.RotateLeft:
                    explorationCommand = new RotateLeftCommand();
                    break;
                case Instruction.MoveForward:
                    explorationCommand = new MoveForwardCommand();
                    break;
            }

            return explorationCommand;
        }
    }
}