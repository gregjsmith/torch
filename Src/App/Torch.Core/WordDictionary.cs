using System;
using System.Linq;

namespace Torch.Core
{
    public class WordDictionary : IWordDictionary
    {
        private readonly string[] _fullContents;

        /// <param name="filePath">Fully-qualified path to the source text file</param>
        /// <param name="dictionaryIo">An <see cref="IDictionaryIO"/> instance used to load the contents of the source file</param>
        public WordDictionary(string filePath, IDictionaryIO dictionaryIo)
        {
            _fullContents = dictionaryIo.GetWordListFrom(filePath);
        }

        /// <summary>
        /// Invoke the provided <see cref="IWordMatchingStrategy"/> to provide the appropriate search results
        /// </summary>
        /// <param name="strategy">The matching strategy in place.</param>
        /// <param name="start">The start point of the search</param>
        /// <param name="end">The end point of the search</param>
        /// <returns>The results of the word search</returns>
        public string[] GetMatches(IWordMatchingStrategy strategy, string start, string end)
        {
            return strategy.GetMatches(GetWorkingSet(start, end), start, end);
        }

        /// <summary>
        /// Get all the words that appear between the provided start and end points within the dictionary. 
        /// The lookup against the dictionary is not case sensitive.
        /// </summary>
        /// <param name="start">This word is the start index of the search</param>
        /// <param name="end">This word is the end index of the search</param>
        /// <returns>
        /// An array of strings including both <see cref="start"/> and <see cref="end"/> and all items in between. 
        /// If <see cref="end"/> appears before start in the dictionary, then the return value from this method will be reversed.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// An <see cref="ArgumentException"/> is thrown if
        /// <list type="bullet">
        ///<item>
        ///<description><see cref="start"/> is not in the dictionary</description>
        ///</item>
        ///<item>
        ///<description><see cref="end"/> is not in the dictionary</description>
        ///</item>
        ///</list>
        /// </exception>
        public string[] GetWorkingSet(string start, string end)
        {
            var first = Array.FindIndex(_fullContents, t => t.IndexOf(start, StringComparison.InvariantCultureIgnoreCase) >= 0);

            if (first == -1)
            {
                throw new ArgumentException("Start word " + start + " was not found in dictionary");
            }

            var last = Array.FindIndex(_fullContents, t => t.IndexOf(end, StringComparison.InvariantCultureIgnoreCase) >= 0);

            if (last == -1)
            {
                throw new ArgumentException("End word " + end + " was not found in dictionary");
            }

            int length, startIndex;
            bool reverse = first > last;

            if (reverse)
            {
                length = (first - last) + 1;
                startIndex = last;
            }
            else
            {
                length = (last - first) + 1;
                startIndex = first;
            }

            var result = new string[length];

            Array.Copy(_fullContents, startIndex, result, 0, length);

            if (reverse)
            {
                return result.Reverse().ToArray();
            }

            return result;
        }
    }
}