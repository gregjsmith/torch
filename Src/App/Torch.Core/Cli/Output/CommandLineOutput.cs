using System;

namespace Torch.Core.Cli.Output
{
    /// <summary>
    /// A container for the output of the command line application
    /// </summary>
    public class CommandLineOutput
    {
        /// <summary>
        /// ANy message the application generates to be output
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The results of invoking the application, if any
        /// </summary>
        public string[] Results { get; set; }

        /// <summary>
        /// The colour that the application should use for the outputs
        /// </summary>
        public ConsoleColor ConsoleColor { get; set; }
    }
}