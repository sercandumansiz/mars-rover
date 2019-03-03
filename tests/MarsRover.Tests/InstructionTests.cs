using System;
using System.Collections.Generic;
using FluentAssertions;
using MarsRover.Program.Common.Enums;
using MarsRover.Program.Models;
using MarsRover.Program.Services;
using MarsRover.Program.Services.Implementations;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MarsRover.Tests
{
    public class InstructionTests
    {
        private IInstructionService _instructionService;
        private ILogger<InstructionService> _loggerMock;
        public InstructionTests()
        {
            _loggerMock = Mock.Of<ILogger<InstructionService>>();
        }

        [Theory]
        [InlineData(null)]
        public void GetInstructions_IsNull_ThrowsValidatePlateauException(string instruction)
        {
            _instructionService = new InstructionService(_loggerMock);

            Assert.Throws<ArgumentNullException>(() => _instructionService.GetInstructions(instruction));
        }

        [Theory]
        [InlineData("LMLMLMLMM")]
        [InlineData("MMRMMRMRRM")]
        public void GetInstructions_IsNotNullAndValidInstruction_ShouldReturnInstructionList_CountShouldEqualWithInstructionLength(string instruction)
        {
            _instructionService = new InstructionService(_loggerMock);

            BaseModel<List<Instruction>> baseInstructionModel = _instructionService.GetInstructions(instruction);

            baseInstructionModel.Data.Count.Should().Equals(instruction.Length);
        }
    }
}