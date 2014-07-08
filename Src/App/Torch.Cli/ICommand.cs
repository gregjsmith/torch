namespace Torch.Cli
{
    /// <summary>
    /// A generic command interface - implementations control the input and output values
    /// </summary>
    public interface ICommand<in TContext, out TResponse>
    {
        /// <summary>
        /// The Context needed in order to execute the command
        /// </summary>
        TContext Context { set; }

        TResponse Execute();
    }
}