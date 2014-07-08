using System;

namespace Torch.Core
{
    public class FileLoadingException : Exception
    {
        public FileLoadingException(string message) : base(message)
        {}
    }
}