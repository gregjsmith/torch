using System;
using System.Collections.Generic;
using Torch.Core;

namespace Torch.Cli
{
    public class ArgumentHandler : IArgumentHandler
    {
        private readonly IApplicationFactory _applicationFactory;

        public ArgumentHandler(IApplicationFactory applicationFactory)
        {
            _applicationFactory = applicationFactory;
        }

        public bool Complete { get; private set; }

        public CommandLineOutput Output { get; private set; }

        public List<Action<CommandLineArguments>> Handlers
        {
            get
            {
                var handlers = new List<Action<CommandLineArguments>>();

                Action<CommandLineArguments> checkForNoArgs = (args) =>
                {
                    if (args.None)
                    {
                        Output = new HelpOutput();
                        Complete = true;
                    }
                };

                Action<CommandLineArguments> checkForAbsentStartOrEndWordForSearch = (args) =>
                {
                    if (args.IsSearchCommand)
                    {
                        if (!args.StartAndEndWordsSpecified)
                        {
                            Output = new InvalidArgumentsOutput();
                            Complete = true;
                        }    
                    }
                };

                Action<CommandLineArguments> checkForInvalidStartOrEndWord = (args) =>
                {
                    if (args.IsSearchCommand)
                    {
                        if (!args.StartWord.IsFourLetterWord())
                        {
                            Output =
                                new InvalidArgumentsOutput("Start word " + args.StartWord + " does not contain exactly four letters.");
                            Complete = true;
                            return;
                        }

                        if (!args.EndWord.IsFourLetterWord())
                        {
                            Output =
                                new InvalidArgumentsOutput("End word " + args.EndWord + " does not contain exactly four letters.");
                            Complete = true;
                        }
                    }

                };

                Action<CommandLineArguments> executeSearchCommand = (args) =>
                {
                    if (args.IsSearchCommand)
                    {
                        var dictionary = _applicationFactory.InitialiseWordDictionaryFor(args.CustomDictionaryFileSpecified ? args.CustomFileLocation : args.DefaultDictionaryFile);

                        var results = dictionary.GetMatches(new SingleLetterUpdateMatching(), args.StartWord, args.EndWord);

                        Output = new CommandLineOutput
                        {
                            Results = results,
                            ConsoleColor = ConsoleColor.Green,
                            Message = results.Length > 0 ? "Results listed below: " : "No results for that search."
                        };

                        Complete = true;
                    }
                };

                handlers.Add(checkForNoArgs);

                handlers.Add(checkForAbsentStartOrEndWordForSearch);
                handlers.Add(checkForInvalidStartOrEndWord);
                handlers.Add(executeSearchCommand);

                return handlers;
            }
        }
    }
}