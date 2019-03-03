using MarsRover.Program.Common.Enums;

namespace MarsRover.Program.Models
{
    public class RoverLocation
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Direction Direction { get; set; }
    }
}