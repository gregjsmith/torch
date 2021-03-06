﻿using System.IO;

namespace Torch.Core
{
    public class WordDictionaryIO : IDictionaryIO
    {
        public string[] GetWordListFrom(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileLoadingException("The file " + file + " does not exist.");
            }
            var contents = File.ReadAllLines(file);

            return contents;
        }

        public void Save(string[] content, string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            File.AppendAllLines(file, content);
        }
    }
}