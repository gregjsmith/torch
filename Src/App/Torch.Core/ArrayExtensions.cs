using System;

namespace Torch.Core
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Check that a given index within an array has a value that isn't null
        /// </summary>
        public static bool HasValue(this string[] array, int index)
        {
            try
            {
                var value = array[index];

                return value != null;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
    }
}