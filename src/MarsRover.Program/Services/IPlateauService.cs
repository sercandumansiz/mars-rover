using MarsRover.Program.Models;
using MarsRover.Program.Common.Exceptions;

namespace MarsRover.Program.Services
{
    public interface IPlateauService
    {
        BaseModel<PlateauModel> Create(int width, int height);
        BaseModel<bool> Validate(PlateauModel plateau);
    }
}