using System.Collections.Generic;
using MarsRover.Program.Common.Enums;
using MarsRover.Program.Models;

namespace MarsRover.Program.Services
{
    public interface IInstructionService
    {
        BaseModel<List<Instruction>> GetInstructions(string instruction);
    }
}