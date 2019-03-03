using System.Collections.Generic;
using MarsRover.Program.Common.Enums;
using MarsRover.Program.Models;

namespace MarsRover.Program.Services
{
    public interface IRoverService
    {
        BaseModel<RoverModel> Initalize(PlateauModel plateauModel, RoverLocation roverLocation);
        BaseModel<bool> ValidateLocation(PlateauModel plateauModel, RoverLocation roverLocation);
        BaseModel<RoverModel> GetCurrentRover();
        void ExplorePlateau(Instruction instruction);
    }
}