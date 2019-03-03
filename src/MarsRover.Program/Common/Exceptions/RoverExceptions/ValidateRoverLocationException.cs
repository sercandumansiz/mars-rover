using System;

namespace MarsRover.Program.Common.Exceptions.PlateauExceptions
{
    public class ValidateRoverLocationException : Exception
    {
        public ValidateRoverLocationException() : base(@"Rover location not validated on the plateau with given parameters.")
        {

        }
    }
}