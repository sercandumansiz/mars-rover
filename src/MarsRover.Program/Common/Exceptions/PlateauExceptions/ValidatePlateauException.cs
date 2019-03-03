using System;

namespace MarsRover.Program.Common.Exceptions.PlateauExceptions
{
    public class ValidatePlateauException : Exception
    {
        public ValidatePlateauException():base(@"Plateau not validated with given parameters.")
        {
            
        }
    }
}