using System;

namespace Torch.Cli
{
    public class InvalidArgumentsOutput : CommandLineOutput
    {
        /// <summary>
        /// Return an output indicating that the arguments passed to the program are not valid, 
        /// optionally specifying a message
        /// </summary>
        public InvalidArgumentsOutput(string customMessage = null)
        {
            if (!string.IsNullOrWhiteSpace(customMessage))
            {
                Message = customMessage;
            }
            else
            {
                Message = Messages.InvalidArguments;                
            }
            ConsoleColor = ConsoleColor.Yellow;
        }
    }
}