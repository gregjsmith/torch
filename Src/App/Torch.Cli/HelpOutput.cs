using System;

namespace Torch.Cli
{
    public class HelpOutput : CommandLineOutput
    {
        public HelpOutput()
        {
            Message = Messages.Help;
            ConsoleColor = ConsoleColor.White;
        }
    }
}