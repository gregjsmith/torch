using Torch.Core;

namespace Torch.Cli
{
    public interface IApplicationFactory
    {
        IWordDictionary InitialiseWordDictionaryFor(string filePath);
        IArgumentHandler InitialiseArgumentHandlers();
    }
}