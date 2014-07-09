namespace Torch.Core.Cli
{
    public class ApplicationFactory : IApplicationFactory
    {

        /// <summary>
        /// Create a concrete <see cref="IWordDictionary"/> implementation with all required dependencies
        /// </summary>
        /// <param name="filePath">The fully qualified path of the file that will act as the source of the dictionary</param>
        public IWordDictionary InitialiseWordDictionaryFor(string filePath)
        {
            return new WordDictionary(filePath, new WordDictionaryIO());
        }

        /// <summary>
        /// Initialises a concrete implementation of an <see cref="IArgumentHandler"/>, including all dependencies.
        /// </summary>
        public IArgumentHandler InitialiseArgumentHandlers()
        {
            return new ArgumentHandler(new SearchCommand(this, new WordDictionaryIO()));
        }
    }

}