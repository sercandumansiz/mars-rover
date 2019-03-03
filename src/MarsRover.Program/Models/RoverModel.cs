using System;
using MarsRover.Program.Common.Enums;

namespace MarsRover.Program.Models
{
    public class RoverModel
    {
        public RoverModel()
        {
            Location = new RoverLocation();
        }
        public Guid Id { get; set; }
        public RoverLocation Location { get; set; }
    }
}