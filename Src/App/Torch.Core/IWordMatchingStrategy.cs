namespace Torch.Core
{
    public interface IWordMatchingStrategy
    {
        string[] GetMatches(string[] set, string start, string end, bool caseSensitiveMatching = false);
    }
}