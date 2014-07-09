using System;
using System.Collections.Generic;

namespace Torch.Core
{
    public class SingleLetterUpdateMatching : IWordMatchingStrategy
    {
        /// <summary>
        /// This matching strategy will inspect an array of words, and test each one against the previous 
        /// entry in the list. Where changing exactly one letter in the previous word makes it equal to the 
        /// current word, the current word is added to the output.
        /// </summary>
        /// <param name="set">This is the set to be tested</param>
        /// <param name="start">Where to start the search</param>
        /// <param name="end">Where to end the search</param>
        /// <param name="caseSensitiveMatching">Whether the matching search should be case sensitive, default is false</param>
        public string[] GetMatches(string[] set, string start, string end, bool caseSensitiveMatching = false)
        {
            var comparisonType = caseSensitiveMatching
                ? StringComparison.CurrentCulture
                : StringComparison.CurrentCultureIgnoreCase;

            var previousWord = start;

            var output = new List<string>();

            foreach (var currentWord in set)
            {
                // Three short-circuit clauses here:
                // 1. stop searching once the end word has been reached
                // 2. the words to compare are already the same
                // 3. word lengths are not equal, no amount of tinkering with the characters will make them equal

                if (currentWord == end) 
                {
                    break;
                }

                if (previousWord == currentWord)
                {
                    continue;
                }

                if (previousWord.Length != currentWord.Length)
                {
                    continue;
                }

                for (int i = 0; i < previousWord.Length; i++)
                {
                    var chars = previousWord.ToCharArray();

                    chars[i] = currentWord[i];

                    var newWord = new string(chars);

                    if (newWord.Equals(currentWord, comparisonType))
                    {
                        output.Add(currentWord); 
                        previousWord = newWord;
                        break;
                    }
                }
            }

            return output.ToArray();
        }
    }
}