using System;
using System.Collections.Generic;
using MarsRover.Program.Common.Enums;
using MarsRover.Program.Common.Exceptions.InstructionException;
using MarsRover.Program.Models;
using Microsoft.Extensions.Logging;

namespace MarsRover.Program.Services.Implementations
{
    public class InstructionService : IInstructionService
    {
        private readonly ILogger<InstructionService> _logger;
        public InstructionService(ILogger<InstructionService> logger)
        {
            _logger = logger;
        }
        public BaseModel<List<Instruction>> GetInstructions(string instruction)
        {
            BaseModel<List<Instruction>> baseInstructionModel = new BaseModel<List<Instruction>>();

            baseInstructionModel.Data = new List<Instruction>();

            if (!string.IsNullOrEmpty(instruction))
            {
                char[] instructions = instruction.ToCharArray();
                foreach (char instruction in instructions)
                {
                    switch (instruction)
                    {
                        case (char)Instruction.RotateRight:
                            baseInstructionModel.Data.Add(Instruction.RotateRight);
                            break;
                        case (char)Instruction.RotateLeft:
                            baseInstructionModel.Data.Add(Instruction.RotateLeft);
                            break;
                        case (char)Instruction.MoveForward:
                            baseInstructionModel.Data.Add(Instruction.MoveForward);
                            break;
                        default:
                            _logger.LogCritical("Invalid instruction.");
                            throw new InvalidInstructionException();
                    }
                }
            }
            else
            {
                _logger.LogError("Instructions not found.");
                throw new ArgumentNullException();
            }
            
            _logger.LogInformation("Instructions settled successfully.");
            return baseInstructionModel;
        }
    }
}
