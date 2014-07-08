namespace Torch.Core
{
    public interface IDictionaryIO
    {
        string[] GetWordListFrom(string file);
        void Save(string[] content, string file);
    }
}