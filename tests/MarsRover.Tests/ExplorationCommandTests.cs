using FluentAssertions;
using MarsRover.Program.Common.Enums;
using MarsRover.Program.ExplorationEngine.Commands;
using MarsRover.Program.ExplorationEngine.Factory;
using MarsRover.Program.Models;
using MarsRover.Program.Services;
using MarsRover.Program.Services.Implementations;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MarsRover.Tests
{
    public class ExplorationCommandTests
    {
        private IRoverService _roverService;
        private IPlateauService _plateauService;
        private IInstructionService _instructionService;
        private IExplorationCommand _explorationCommandMock;
        private IExplorationFactory _explorationFactoryMock;
        private ILogger<InstructionService> _loggerInstructionMock;
        private ILogger<RoverService> _loggerRoverMock;
        private ILogger<PlateauService> _loggerPlateauMock;
        public ExplorationCommandTests()
        {
            _explorationFactoryMock = Mock.Of<IExplorationFactory>();
            _loggerRoverMock = Mock.Of<ILogger<RoverService>>();
            _loggerPlateauMock = Mock.Of<ILogger<PlateauService>>();
            _loggerInstructionMock = Mock.Of<ILogger<InstructionService>>();
            _explorationCommandMock = Mock.Of<IExplorationCommand>();
        }

        [Theory]
        [InlineData(5, 5, 1, 2, Direction.West, Direction.North)]
        [InlineData(2, 2, 0, 0, Direction.North, Direction.East)]
        [InlineData(8, 7, 8, 7, Direction.East, Direction.South)]
        [InlineData(4, 5, 3, 5, Direction.South, Direction.West)]
        public void RotateRightCommand_ValidPlateauValidRoverAndValidInstructions_ShouldBeEqualGivenResult(int width, int height, int xPosition, int yPosition, Direction direction, Direction result)
        {
            _plateauService = new PlateauService(_loggerPlateauMock);
            _roverService = new RoverService(_loggerRoverMock, _explorationFactoryMock);
            _instructionService = new InstructionService(_loggerInstructionMock);

            BaseModel<PlateauModel> basePlateauModel = _plateauService.Create(width, height);

            basePlateauModel.Data.Width.Should().Equals(width);
            basePlateauModel.Data.Height.Should().Equals(height);

            RoverLocation roverLocation = new RoverLocation()
            {
                Direction = direction,
                XPosition = xPosition,
                YPosition = yPosition
            };

            BaseModel<RoverModel> baseRoverModel = _roverService.Initalize(basePlateauModel.Data, roverLocation);

            _explorationCommandMock = new RotateRightCommand();

            _explorationCommandMock.Explore(_roverService.GetCurrentRover().Data.Location);

            _roverService.GetCurrentRover().Data.Location.Direction.Should().Be(result);
        }

        [Theory]
        [InlineData(5, 5, 1, 2, Direction.West, Direction.South)]
        [InlineData(2, 2, 0, 0, Direction.North, Direction.West)]
        [InlineData(8, 7, 8, 7, Direction.East, Direction.North)]
        [InlineData(4, 5, 3, 5, Direction.South, Direction.East)]
        public void RotateLeftCommand_ValidPlateauValidRoverAndValidInstructions_ShouldBeEqualGivenResult(int width, int height, int xPosition, int yPosition, Direction direction, Direction result)
        {
            _plateauService = new PlateauService(_loggerPlateauMock);
            _roverService = new RoverService(_loggerRoverMock, _explorationFactoryMock);
            _instructionService = new InstructionService(_loggerInstructionMock);

            BaseModel<PlateauModel> basePlateauModel = _plateauService.Create(width, height);

            basePlateauModel.Data.Width.Should().Equals(width);
            basePlateauModel.Data.Height.Should().Equals(height);

            RoverLocation roverLocation = new RoverLocation()
            {
                Direction = direction,
                XPosition = xPosition,
                YPosition = yPosition
            };

            BaseModel<RoverModel> baseRoverModel = _roverService.Initalize(basePlateauModel.Data, roverLocation);

            _explorationCommandMock = new RotateLeftCommand();

            _explorationCommandMock.Explore(_roverService.GetCurrentRover().Data.Location);

            _roverService.GetCurrentRover().Data.Location.Direction.Should().Be(result);
        }
    }
}