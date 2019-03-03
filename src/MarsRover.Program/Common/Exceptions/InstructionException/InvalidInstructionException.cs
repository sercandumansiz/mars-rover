using System;

namespace MarsRover.Program.Common.Exceptions.InstructionException
{
    public class InvalidInstructionException : Exception
    {
        public InvalidInstructionException() : base(@"Invalid instruction.")
        {

        }
    }
}