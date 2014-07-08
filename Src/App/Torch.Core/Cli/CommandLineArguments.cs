using System;

namespace Torch.Core.Cli
{
    /// <summary>
    /// Controls access to the list of arguments passed at the command line
    /// </summary>
    public class CommandLineArguments
    {
        private readonly string[] _args;

        public CommandLineArguments(string[] args)
        {
            _args = args;
        }

        public bool Any
        {
            get { return _args.Length > 0; }
        }

        public bool None
        {
            get { return _args.Length == 0; }
        }


        /// <summary>
        /// Test whether the first argument is "search", ignoring case
        /// </summary>
        public bool IsSearchCommand
        {
            get { return _args[0].ToLower() == "search"; }
        }

        /// <summary>
        /// Test whether a start word has been specified at position 1
        /// </summary>
        public bool StartWordSpecified
        {
            get { return _args.Length > 1 && _args[1] != null; }
        }

        /// <summary>
        /// The start word specified at position 1
        /// </summary>
        public string StartWord
        {
            get { return _args[1]; }
        }

        /// <summary>
        /// Test whether an End word has been specified at position 2
        /// </summary>
        public bool EndWordSpecified
        {
            get { return _args.Length > 2 && _args[2] != null; }
        }

        /// <summary>
        /// The end word specified at position 2
        /// </summary>
        public string EndWord
        {
            get { return _args[2]; }
        }

        /// <summary>
        /// Test whether both a start and end word have been specified
        /// </summary>
        public bool StartAndEndWordsSpecified
        {
            get { return StartWordSpecified && EndWordSpecified; }
        }


        /// <summary>
        /// Whether a custom file location has been passed at the command line
        /// </summary>
        public bool CustomDictionaryFileSpecified
        {
            get
            {
                var specced = Array.IndexOf(_args, "-f");

                return specced != -1;
            }
        }

        public string CustomFileLocation
        {
            get
            {
                var f = Array.IndexOf(_args, "-f");

                if (!_args.HasValue(f+1) || !_args[f+1].IsAValidTextFileName())
                {
                    throw new ArgumentException("A -f switch was included but the following argument was not valid. A custom dictionary file argument must be the path to a text file.");
                }

                var file = _args[f + 1];

                return file;
            }
        }

        public bool OutputFileSpecified
        {
            get
            {
                var specced = Array.IndexOf(_args, "-o");

                return specced != -1;
            }
        }

        public string OutputFileLocation
        {
            get
            {
                var f = Array.IndexOf(_args, "-o");

                if (!_args.HasValue(f + 1) || !_args[f + 1].IsAValidTextFileName())
                {
                    throw new ArgumentException("A -o switch was included but the following argument was not valid. An output file must be the path to a text file.");
                }

                var file = _args[f + 1];

                return file;
            }


        }

        public string DefaultDictionaryFile
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + @"Assets\Dictionary.txt"; }
        }
    }
}