using System.Text.RegularExpressions;

namespace Torch.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// Test whether a string is exactly four characters in length and contains only alphabetic letters
        /// </summary>
        public static bool IsFourLetterWord(this string str)
        {
            if (str.Length != 4)
            {
                return false;
            }

            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }

        /// <summary>
        /// Check whether a string value is a valid text file name by ensuring it has a .txt extension
        /// </summary>
        public static bool IsAValidTextFileName(this string fileName)
        {
            if (!fileName.Contains(".txt"))
            {
                return false;
            }

            return true;
        }
    }
}