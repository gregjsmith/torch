namespace Torch.Core.Cli
{
    public interface IApplicationFactory
    {
        IWordDictionary InitialiseWordDictionaryFor(string filePath);
        IArgumentHandler InitialiseArgumentHandlers();
    }
}