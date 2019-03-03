using System;
using System.Collections.Generic;
using MarsRover.Program.Common.Enums;
using MarsRover.Program.ExplorationEngine.Commands;
using MarsRover.Program.ExplorationEngine.Factory;
using MarsRover.Program.Models;
using MarsRover.Program.Services;
using MarsRover.Program.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MarsRover.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<IExplorationFactory, ExplorationFactory>()
            .AddSingleton<IExplorationCommand, RotateRightCommand>()
            .AddSingleton<IExplorationCommand, RotateLeftCommand>()
            .AddSingleton<IExplorationCommand, MoveForwardCommand>()
            .AddSingleton<IPlateauService, PlateauService>()
            .AddSingleton<IRoverService, RoverService>()
            .AddSingleton<IInstructionService, InstructionService>()
            .BuildServiceProvider();

            ConfigureLogger(serviceProvider);

            IPlateauService plateauService = serviceProvider.GetService<IPlateauService>();
            IRoverService roverService = serviceProvider.GetService<IRoverService>();
            IInstructionService instructionService = serviceProvider.GetService<IInstructionService>();

            List<RoverModel> rovers = new List<RoverModel>();

            BaseModel<PlateauModel> basePlateauModel = plateauService.Create(5, 5);
            RoverLocation firstRoverLocation = new RoverLocation()
            {
                XPosition = 1,
                YPosition = 2,
                Direction = Direction.North
            };

            BaseModel<RoverModel> baseRoverModel = roverService.Initalize(basePlateauModel.Data, firstRoverLocation);
            rovers.Add(baseRoverModel.Data);

            BaseModel<List<Instruction>> baseInstructionModel = instructionService.GetInstructions("LMLMLMLMM");

            foreach (Instruction instruction in baseInstructionModel.Data)
            {
                roverService.ExplorePlateau(instruction);
            }

            RoverLocation secondRoverLocation = new RoverLocation()
            {
                XPosition = 3,
                YPosition = 3,
                Direction = Direction.East
            };

            baseRoverModel = roverService.Initalize(basePlateauModel.Data, secondRoverLocation);
            rovers.Add(baseRoverModel.Data);

            baseInstructionModel = instructionService.GetInstructions("MMRMMRMRRM");

            foreach (Instruction instruction in baseInstructionModel.Data)
            {
                roverService.ExplorePlateau(instruction);
            }

        }


        public static void ConfigureLogger(ServiceProvider serviceProvider)
        {
            serviceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);
            ILogger logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
        }
    }
}
