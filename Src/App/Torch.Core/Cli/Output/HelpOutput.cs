using System;

namespace Torch.Core.Cli.Output
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