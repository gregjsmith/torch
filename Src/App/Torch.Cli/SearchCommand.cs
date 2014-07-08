using Torch.Core;

namespace Torch.Cli
{
    /// <summary>
    /// Encapsulates the execution of a dictionary search operation
    /// </summary>
    public class SearchCommand : ICommand<CommandLineArguments, SearchCommandResult>
    {
        private readonly IApplicationFactory _applicationFactory;
        private readonly IDictionaryIO _dictionaryIo;
        private CommandLineArguments _args;

        public SearchCommand(IApplicationFactory applicationFactory, IDictionaryIO dictionaryIo)
        {
            _applicationFactory = applicationFactory;
            _dictionaryIo = dictionaryIo;
        }

        public CommandLineArguments Context { set { _args = value; } }


        /// <summary>
        /// Execute a dictionary seach operation. This will search the provided dictionary file, get matching results, 
        /// and optionally output these results to a file.
        /// </summary>
        public SearchCommandResult Execute()
        {
            string outcome;

            var dictionary = _applicationFactory.InitialiseWordDictionaryFor(_args.CustomDictionaryFileSpecified ? _args.CustomFileLocation : _args.DefaultDictionaryFile);

            var results = dictionary.GetMatches(new SingleLetterUpdateMatching(), _args.StartWord, _args.EndWord);

            if (_args.OutputFileSpecified)
            {
                _dictionaryIo.Save(results, _args.OutputFileLocation); 
                outcome = "Results saved to " + _args.OutputFileLocation;
            }
            else
            {
                outcome = results.Length > 0 ? "Results listed below: " : "No results for that search.";
            }

            return new SearchCommandResult
            {
                Results = results,
                Outcome = outcome
            };
        }
    }
}