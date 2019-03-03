using System;
using FluentAssertions;
using MarsRover.Program.Common.Enums;
using MarsRover.Program.Common.Exceptions.PlateauExceptions;
using MarsRover.Program.ExplorationEngine.Factory;
using MarsRover.Program.Models;
using MarsRover.Program.Services;
using MarsRover.Program.Services.Implementations;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MarsRover.Tests
{
    public class RoverTests
    {
        private IRoverService _roverService;
        private IPlateauService _plateauService;
        private ILogger<RoverService> _loggerRoverMock;
        private ILogger<PlateauService> _loggerPlateauMock;
        private IExplorationFactory _explorationFactoryMock;
        public RoverTests()
        {
            _explorationFactoryMock = Mock.Of<IExplorationFactory>();
            _loggerRoverMock = Mock.Of<ILogger<RoverService>>();
            _loggerPlateauMock = Mock.Of<ILogger<PlateauService>>();
        }

        [Theory]
        [InlineData(5, 5, 1, 2, Direction.West)]
        [InlineData(2, 2, 0, 0, Direction.North)]
        [InlineData(8, 7, 8, 7, Direction.East)]
        [InlineData(4, 5, 3, 5, Direction.South)]
        public void Initialize_ValidPlateauAndValidPositionWithValidDirection_ShouldReturnGivenLocation(int width, int height, int xPosition, int yPosition, Direction direction)
        {
            _plateauService = new PlateauService(_loggerPlateauMock);
            _roverService = new RoverService(_loggerRoverMock, _explorationFactoryMock);

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

            baseRoverModel.Data.Id.Should().Equals(_roverService.GetCurrentRover().Data.Id);
            baseRoverModel.Data.Location.Direction.Should().Equals(roverLocation.Direction);
            baseRoverModel.Data.Location.XPosition.Should().Equals(roverLocation.XPosition);
            baseRoverModel.Data.Location.YPosition.Should().Equals(roverLocation.YPosition);
        }

        [Theory]
        [InlineData(5, 5, 6, -1, Direction.West)]
        [InlineData(2, 2, 4, 4, Direction.North)]
        [InlineData(8, 7, 4, 15, Direction.East)]
        [InlineData(4, 5, -2, 5, Direction.South)]
        public void ValidateLocation_ValidPlateauAndOutsidePositionWithValidDirection_ThrowsValidateRoverLocationException(int width, int height, int xPosition, int yPosition, Direction direction)
        {
            _plateauService = new PlateauService(_loggerPlateauMock);
            _roverService = new RoverService(_loggerRoverMock, _explorationFactoryMock);

            BaseModel<PlateauModel> basePlateauModel = _plateauService.Create(width, height);

            basePlateauModel.Data.Width.Should().Equals(width);
            basePlateauModel.Data.Height.Should().Equals(height);

            RoverLocation roverLocation = new RoverLocation()
            {
                Direction = direction,
                XPosition = xPosition,
                YPosition = yPosition
            };

            Assert.Throws<ValidateRoverLocationException>(() => _roverService.ValidateLocation(basePlateauModel.Data, roverLocation));
        }
    }
}