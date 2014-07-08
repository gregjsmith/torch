using Torch.Core;

namespace Torch.Cli
{
    /// <summary>
    /// Encapsulates the execution of a dictionary search operation
    /// </summary>
    public class SearchCommand : ICommand<CommandLineArguments, string[]>
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

        public string[] Execute()
        {
            var dictionary = _applicationFactory.InitialiseWordDictionaryFor(_args.CustomDictionaryFileSpecified ? _args.CustomFileLocation : _args.DefaultDictionaryFile);

            var results = dictionary.GetMatches(new SingleLetterUpdateMatching(), _args.StartWord, _args.EndWord);

            if (_args.OutputFileSpecified)
            {
                _dictionaryIo.Save(results, _args.OutputFileLocation); //TODO(Greg) pass a massage back up the stack.
            }

            return results;
        }
    }
}