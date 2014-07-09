namespace Torch.Core
{
    public interface IWordDictionary
    {

        string[] GetWorkingSet(string start, string end);

        string[] GetMatches(IWordMatchingStrategy strategy, string start, string end);
    }
}