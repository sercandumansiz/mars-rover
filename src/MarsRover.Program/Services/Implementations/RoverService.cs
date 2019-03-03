using System;
using MarsRover.Program.Common.Enums;
using MarsRover.Program.Common.Exceptions.PlateauExceptions;
using MarsRover.Program.ExplorationEngine.Factory;
using MarsRover.Program.Models;
using Microsoft.Extensions.Logging;

namespace MarsRover.Program.Services.Implementations
{
    public class RoverService : IRoverService
    {
        private RoverModel CurrentRover { get; set; }
        private readonly ILogger<RoverService> _logger;
        private readonly IExplorationFactory _explorationFactory;
        public RoverService(ILogger<RoverService> logger, IExplorationFactory explorationFactory)
        {
            _logger = logger;
            _explorationFactory = explorationFactory;
        }

        public BaseModel<RoverModel> Initalize(PlateauModel plateauModel, RoverLocation roverLocation)
        {
            BaseModel<RoverModel> model = new BaseModel<RoverModel>();

            BaseModel<bool> validateLocationModel = ValidateLocation(plateauModel, roverLocation);

            if (validateLocationModel.Data)
            {
                model.Data = new RoverModel();
                model.Data.Id = Guid.NewGuid();
                model.Data.Location.Direction = roverLocation.Direction;
                model.Data.Location.XPosition = roverLocation.XPosition;
                model.Data.Location.YPosition = roverLocation.YPosition;
                CurrentRover = model.Data;
                _logger.LogInformation("Rover initialized successfully.");
            }

            return model;
        }
        public BaseModel<bool> ValidateLocation(PlateauModel plateauModel, RoverLocation roverLocation)
        {
            BaseModel<bool> model = new BaseModel<bool>();

            bool isValidDirection = IsValidDirection(roverLocation.Direction);
            bool isValidXPosition = IsValidXPosition(plateauModel.Width, roverLocation.XPosition);
            bool isValidYPosition = IsValidYPosition(plateauModel.Height, roverLocation.YPosition);

            if (isValidDirection && isValidXPosition && isValidYPosition)
            {
                model.Data = true;
                _logger.LogInformation($"Given location ({roverLocation.XPosition}-{roverLocation.YPosition}-{roverLocation.Direction}) for the rover is valid on the given plateau ({plateauModel.Width}-{plateauModel.Height}).");
            }
            else
            {
                ValidateRoverLocationException exception = new ValidateRoverLocationException();
                _logger.LogError(exception.Message);
                throw exception;
            }

            return model;
        }

        public BaseModel<RoverModel> GetCurrentRover()
        {
            BaseModel<RoverModel> model = new BaseModel<RoverModel>();

            if (CurrentRover != null)
            {
                model.Data = CurrentRover;
            }
            else
            {
                model.Errors.Add("Rover not found.");
            }

            return model;
        }

        public void ExplorePlateau(Instruction instruction)
        {
            _explorationFactory.ExecuteInstruction(instruction).Explore(CurrentRover.Location);
            _logger.LogInformation($"Rover location is ({CurrentRover.Location.XPosition}-{CurrentRover.Location.YPosition}-{CurrentRover.Location.Direction})");
        }

        private bool IsValidDirection(Direction direction)
        {
            if (direction == Direction.West ||
                direction == Direction.East ||
                direction == Direction.North ||
                direction == Direction.South)
            {
                return true;
            }

            return false;
        }
        private bool IsValidXPosition(int width, int xPosition)
        {
            return xPosition >= 0 && xPosition <= width;
        }
        private bool IsValidYPosition(int height, int yPosition)
        {
            return yPosition >= 0 && yPosition <= height;
        }
    }
}