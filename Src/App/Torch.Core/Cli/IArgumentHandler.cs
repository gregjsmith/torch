using System;
using System.Collections.Generic;
using Torch.Core.Cli.Output;

namespace Torch.Core.Cli
{
    public interface IArgumentHandler
    {
        /// <summary>
        /// Whether the arguments have been successfully handled yet or not.
        /// </summary>
        bool Complete { get; }

        /// <summary>
        /// The output created by the apropriate handler
        /// </summary>
        CommandLineOutput Output { get;}

        /// <summary>
        /// A list of functions that process a <see cref="CommandLineArguments"/> object
        /// </summary>
        List<Action<CommandLineArguments>> Handlers { get; }
    }
}